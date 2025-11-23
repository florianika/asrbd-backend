
using Application.Exceptions;
using Application.FieldWork.ExecuteJob.Response;
using Application.FieldWork.TestUntestedBld.Request;
using Application.FieldWork.TestUntestedBld.Response;
using Application.Ports;
using Hangfire;
using Microsoft.Extensions.Logging;

namespace Application.FieldWork.TestUntestedBld
{
    public class TestUntestedBld : ITestUntestedBld
    {
        private readonly ILogger _logger;
        private readonly IFieldWorkRepository _fieldWorkRepository;
        public TestUntestedBld(ILogger<TestUntestedBld> logger, IFieldWorkRepository fieldWorkRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _fieldWorkRepository = fieldWorkRepository ?? throw new ArgumentNullException(nameof(fieldWorkRepository));
        }

        public async Task<TestUntestedBldResponse> Execute(TestUntestedBldRequest request)
        {
            try
            {
                var job = new Domain.Jobs
                {
                    FieldWorkId = request.Id == 0 ? null : request.Id,
                    CreatedUser = request.CreatedUser,
                    CreatedTimestamp = DateTime.Now,
                    Status = Domain.Enum.JobStatus.RUNNING,
                    CompletedTimestamp = null
                };

                var jobId = await _fieldWorkRepository.CreateJob(job);

                // Hangfire enqueue
                BackgroundJob.Enqueue<IJobExecutor>(executor => executor.ExecuteTestUntestedBldJobAsync(jobId));

                return new TestUntestedBldSuccessResponse
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
