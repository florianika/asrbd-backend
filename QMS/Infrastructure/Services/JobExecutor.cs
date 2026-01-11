using Application.Exceptions;
using Application.Ports;
using Hangfire;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services
{
    public class JobExecutor : IJobExecutor
    {
        private readonly IFieldWorkRepository _fieldWorkrepository;
        private readonly ILogger<JobExecutor> _logger;
        private readonly IBuildingRepository _buildingRepository;

        public JobExecutor(IFieldWorkRepository repository, ILogger<JobExecutor> logger, IBuildingRepository buildingRepository)
        {
            _fieldWorkrepository = repository;
            _logger = logger;
            _buildingRepository = buildingRepository;
        }

        public async Task ExecuteStatisticsJobAsync(int jobId)
        {
            try
            {
                await _fieldWorkrepository.ExecuteStatisticsSP(jobId);

                var job = await _fieldWorkrepository.GetJobById(jobId);
                job.Status = Domain.Enum.JobStatus.COMPLETED;
                job.CompletedTimestamp = DateTime.Now;
                await _fieldWorkrepository.UpdateJob(job);
            }
            catch (AppException ex)
            {
                _logger.LogError(ex, "Job {JobId} failed", jobId);
                var job = await _fieldWorkrepository.GetJobById(jobId);
                job.Status = Domain.Enum.JobStatus.FAILED;
                job.CompletedTimestamp = DateTime.Now;
                await _fieldWorkrepository.UpdateJob(job);
            }
        }
        public async Task ExecuteTestUntestedBldJobAsync(int jobId)
        {
            try
            {
                await _fieldWorkrepository.ExecuteTestUntestedBldSP(jobId);

                var job = await _fieldWorkrepository.GetJobById(jobId);
                job.Status = Domain.Enum.JobStatus.COMPLETED;
                job.CompletedTimestamp = DateTime.Now;
                await _fieldWorkrepository.UpdateJob(job);
            }
            catch (AppException ex)
            {
                _logger.LogError(ex, "Job {JobId} failed", jobId);
                var job = await _fieldWorkrepository.GetJobById(jobId);
                job.Status = Domain.Enum.JobStatus.FAILED;
                job.CompletedTimestamp = DateTime.Now;
                await _fieldWorkrepository.UpdateJob(job);
            }
        }
        [DisableConcurrentExecution(timeoutInSeconds: 3600)]
        [AutomaticRetry(Attempts = 0, OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public async Task ExecuteTestBuildingsAsync(int jobId, bool isAllBuildings, bool runUpdates)
        {
            try
            {
                await _buildingRepository.ExecuteTestBuildingSP(jobId, isAllBuildings,runUpdates);

                //var job = await _buildingRepository.GetJobById(jobId);
                //job.Status = Domain.Enum.JobStatus.COMPLETED;
                //job.CompletedTimestamp = DateTime.Now;
                //await _buildingRepository.UpdateJob(job);
                // e bej ne stored procedure
            }
            catch (AppException ex)
            {
                _logger.LogError(ex, "Job {JobId} failed", jobId);
                
            }
        }
    }

}
