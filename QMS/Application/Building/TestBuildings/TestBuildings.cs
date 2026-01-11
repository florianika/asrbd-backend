
using Application.Building.TestBuildings.Request;
using Application.Building.TestBuildings.Response;
using Application.Exceptions;
using Application.Ports;
using Hangfire;
using Microsoft.Extensions.Logging;

namespace Application.Building.TestBuildings
{
    public class TestBuildings : ITestBuildings
    {
        private readonly ILogger _logger;
        private readonly IBuildingRepository _buildingRepository;
        public TestBuildings(ILogger<TestBuildings> logger, IBuildingRepository buildingRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _buildingRepository = buildingRepository ?? throw new ArgumentNullException(nameof(buildingRepository));
        }
        public async Task<TestBuildingsResponse> Execute(TestBuildingsRequest request)
        {
            try
            {
                //chech if there are running job
                var runningJobs = await _buildingRepository.GetRunningJobs();

                if (runningJobs.Any())
                {
                    // There is a job already running
                    throw new PreconditionFailedException("There is a job already running");
                }

                var job = new Domain.Jobs
                {
                    FieldWorkId = null,
                    CreatedUser = request.CreatedUser,
                    CreatedTimestamp = DateTime.Now,
                    Status = Domain.Enum.JobStatus.RUNNING,
                    CompletedTimestamp = null
                };

                var jobId = await _buildingRepository.CreateJob(job);

                //// Hangfire enqueue
                //BackgroundJob.Enqueue<IJobExecutor>(executor => executor.ExecuteTestBuildingsAsync(jobId, request.isAllBuildings));
                TimeSpan delay = TimeSpan.Zero;
                if (request.StartAt.HasValue)
                {
                    var runAt = request.StartAt.Value;
                    delay = runAt - DateTime.Now;
                    if (delay < TimeSpan.Zero)
                        delay = TimeSpan.Zero; // if is in the past, run immediately
                }
                var hangfireJobId = string.Empty;
                if (delay == TimeSpan.Zero)
                {
                    // run immediately
                    hangfireJobId = BackgroundJob.Enqueue<IJobExecutor>(
                        executor => executor.ExecuteTestBuildingsAsync(jobId, request.isAllBuildings, request.RunUpdates));
                }
                else
                {
                    // ekzekutim i skeduluar
                    hangfireJobId = BackgroundJob.Schedule<IJobExecutor>(
                        executor => executor.ExecuteTestBuildingsAsync(jobId, request.isAllBuildings, request.RunUpdates),
                        delay);
                }
                return new TestBuildingsSuccessResponse
                {
                    HangfireJobId = hangfireJobId,
                    JobId = jobId
                };
            }
            catch (AppException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
