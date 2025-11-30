
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
                //kontrollo nese ka job running
                var runningJobs = await _buildingRepository.GetRunningJobs();

                if (runningJobs.Any())
                {
                    // ka job-e që po ekzekutohen
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
                        delay = TimeSpan.Zero; // nëse ora ka kaluar, ekzekutoje menjëherë
                }

                if (delay == TimeSpan.Zero)
                {
                    // ekzekutim i menjëhershëm
                    BackgroundJob.Enqueue<IJobExecutor>(
                        executor => executor.ExecuteTestBuildingsAsync(jobId, request.isAllBuildings));
                }
                else
                {
                    // ekzekutim i skeduluar
                    BackgroundJob.Schedule<IJobExecutor>(
                        executor => executor.ExecuteTestBuildingsAsync(jobId, request.isAllBuildings),
                        delay);
                }
                return new TestBuildingsSuccessResponse
                {
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
