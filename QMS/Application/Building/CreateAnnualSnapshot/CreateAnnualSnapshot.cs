using Application.Building.CreateAnnualSnapshot.Request;
using Application.Building.CreateAnnualSnapshot.Response;
using Application.Exceptions;
using Application.Ports;
using Domain;
using Domain.Enum;
using Hangfire;
using Microsoft.Extensions.Logging;

namespace Application.Building.CreateAnnualSnapshot
{
    public class CreateAnnualSnapshot : ICreateAnnualSnapshot
    {
        private readonly IDownloadJobRepository _downloadJobRepository;
        private readonly ILogger _logger;
        public CreateAnnualSnapshot(IDownloadJobRepository downloadJobRepository, ILogger<CreateAnnualSnapshot> logger)
        {
            _downloadJobRepository = downloadJobRepository ?? throw new ArgumentNullException(nameof(downloadJobRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<CreateAnnualSnapshotResponse> Execute(CreateAnnualSnapshotRequest request)
        {
            var existingJob = await _downloadJobRepository.GetDownloadJobByYear(request.ReferenceYear);
            if (existingJob != null)
            {
                // 1.a — it exists and is running → error
                if (existingJob.Status == DownloadStatus.RUNNING)
                {
                    throw new PreconditionFailedException(
                        $"Export for {request.ReferenceYear} is already running.");
                }

                // 1.b — if exists and is not running → create it
                existingJob.Status = DownloadStatus.PENDING;
                existingJob.LastUpdatedBy = request.CreatedBy;
                existingJob.FileUrl = null;
                existingJob.CompletedAt = null;
                existingJob.Remarks += DateTime.UtcNow.ToString("dd.MM.yyyy HH:mm:ss") + ": " + request.Remarks + Environment.NewLine;

                await _downloadJobRepository.UpdateDownloadJob(existingJob);

                // 2. Enqueue Hangfire job
                BackgroundJob.Enqueue<IAnnualSnapshotExecutor>(executor =>
                    executor.ExportAnnualSnapshotAsync(existingJob.Id));

                return new CreateAnnualSnapshotSuccessResponse
                {
                    DownloadJobId = existingJob.Id
                };
            }

            // 3. if does not exists, create a new one
            var newJob = new DownloadJob
            {
                ReferenceYear = request.ReferenceYear,
                Status = DownloadStatus.PENDING,
                CreatedBy = request.CreatedBy,
                CreatedAt = DateTime.UtcNow,
                FileUrl = null,
                CompletedAt = null,
                Remarks = DateTime.UtcNow.ToString("dd.MM.yyyy HH:mm:ss") + ": " + request.Remarks + Environment.NewLine
            };

            await _downloadJobRepository.CreateDownloadJob(newJob);

            // 4. Enqueue Hangfire job
            BackgroundJob.Enqueue<IAnnualSnapshotExecutor>(executor =>
                executor.ExportAnnualSnapshotAsync(newJob.Id));

            return new CreateAnnualSnapshotSuccessResponse
            {
                DownloadJobId = newJob.Id
            };
        }
    }
}
