
using Application.Exceptions;
using Application.FieldWork.ExecuteJob.Request;
using Application.FieldWork.ExecuteJob.Response;
using Application.Ports;
using Hangfire;
using Microsoft.Extensions.Logging;

namespace Application.FieldWork.ExecuteJob
{
    public class ExecuteJob : IExecuteJob
    {
        private readonly ILogger _logger;
        private readonly IFieldWorkRepository _fieldWorkRepository;

        public ExecuteJob(ILogger<ExecuteJob> logger, IFieldWorkRepository fieldWorkRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _fieldWorkRepository = fieldWorkRepository ?? throw new ArgumentNullException(nameof(fieldWorkRepository));
        }

        public async Task<ExecuteJobResponse> Execute(ExecuteJobRequest request)
        {
            try
            {
                var job = new Domain.Jobs
                {
                    FieldWorkId = request.Id,
                    CreatedUser = request.CreatedUser,
                    CreatedTimestamp = DateTime.Now,
                    Status = Domain.Enum.JobStatus.RUNNING,
                    CompletedTimestamp = null
                };

                var jobId = await _fieldWorkRepository.CreateJob(job);

                // Hangfire enqueue
                BackgroundJob.Enqueue<IJobExecutor>(executor => executor.ExecuteStatisticsJobAsync(jobId));

                return new ExecuteJobSuccessResponse
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
