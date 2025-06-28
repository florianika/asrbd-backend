using Application.Exceptions;
using Application.Ports;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services
{
    public class JobExecutor : IJobExecutor
    {
        private readonly IFieldWorkRepository _repository;
        private readonly ILogger<JobExecutor> _logger;

        public JobExecutor(IFieldWorkRepository repository, ILogger<JobExecutor> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task ExecuteStatisticsJobAsync(int jobId)
        {
            try
            {
                await _repository.ExecuteStatisticsSP(jobId);

                var job = await _repository.GetJobById(jobId);
                job.Status = Domain.Enum.JobStatus.COMPLETED;
                job.CompletedTimestamp = DateTime.Now;
                await _repository.UpdateJob(job);
            }
            catch (AppException ex)
            {
                _logger.LogError(ex, "Job {JobId} failed", jobId);
                var job = await _repository.GetJobById(jobId);
                job.Status = Domain.Enum.JobStatus.FAILED;
                job.CompletedTimestamp = DateTime.Now;
                await _repository.UpdateJob(job);
            }
        }
    }

}
