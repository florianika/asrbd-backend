using Application.Exceptions;
using Application.Ports;
using Domain.Enum;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;
using System.IO.Compression;
using System.Text;

namespace Infrastructure.Services
{
    public class AnnualSnapshotExecutor : IAnnualSnapshotExecutor
    {
        private readonly IDownloadJobRepository _downloadJobRepository;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<AnnualSnapshotExecutor> _logger;

        public AnnualSnapshotExecutor(
            IDownloadJobRepository downloadJobRepository,
            IConfiguration configuration,
            IWebHostEnvironment env,
            ILogger<AnnualSnapshotExecutor> logger)
        {
            _downloadJobRepository = downloadJobRepository ?? throw new ArgumentNullException(nameof(downloadJobRepository));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _env = env ?? throw new ArgumentNullException(nameof(env));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [Hangfire.DisableConcurrentExecution(timeoutInSeconds: 3600)]
        public async Task ExportAnnualSnapshotAsync(int downloadJobId)
        {
            var job = await _downloadJobRepository.GetDownloadJobById(downloadJobId);
            if (job == null)
            {
                _logger.LogError("DownloadJob with id {JobId} not found.", downloadJobId);
                throw new NotFoundException($"DownloadJob with id {downloadJobId} not found.");
            }

            job.Status = DownloadStatus.RUNNING;
            await _downloadJobRepository.UpdateDownloadJob(job);

            var connectionString = _configuration.GetConnectionString("QMSConnectionString");
            //The connection string is used, becasue we will use SqlDataReader for reason that there are many rows and we should not keep all in memory
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                _logger.LogError("Connection string 'QMSConnectionString' is not configured.");
                job.Status = DownloadStatus.FAILED;
                job.CompletedAt = DateTime.UtcNow;
                await _downloadJobRepository.UpdateDownloadJob(job);
                throw new NotFoundException($"Connection string is missing");
            }
            try
            {
                var exportRoot = GetExportRoot();
                Directory.CreateDirectory(exportRoot);

                var zipFileName = $"annual-snapshot_{job.ReferenceYear}.zip";
                var zipPath = Path.Combine(exportRoot, zipFileName);

                if (File.Exists(zipPath))
                    File.Delete(zipPath);

                using (var conn = new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();

                    using (var zipStream = new FileStream(zipPath, FileMode.CreateNew, FileAccess.Write, FileShare.None))
                    using (var zip = new ZipArchive(zipStream, ZipArchiveMode.Create, leaveOpen: false, entryNameEncoding: Encoding.UTF8))
                    {
                        await WriteCsvFromStoredProcAsync(
                            zip,
                            entryName: "buildings.csv",
                            connection: conn,
                            storedProcName: "dbo.ExportAnnualBuildingsSnapshot", 
                            referenceYear: job.ReferenceYear,
                            commandTimeoutSeconds: 1800);

                        await WriteCsvFromStoredProcAsync(
                            zip,
                            entryName: "dwellings.csv",
                            connection: conn,
                            storedProcName: "dbo.ExportAnnualDwellingsSnapshot", 
                            referenceYear: job.ReferenceYear,
                            commandTimeoutSeconds: 1800);

                        await WriteCsvFromStoredProcAsync(
                            zip,
                            entryName: "quality-report.csv",
                            connection: conn,
                            storedProcName: "dbo.ExportAnnualQualitySnapshot", 
                            referenceYear: job.ReferenceYear,
                            commandTimeoutSeconds: 1800);
                    }
                }

                job.Status = DownloadStatus.COMPLETED;
                job.CompletedAt = DateTime.UtcNow;
                job.FileUrl = $"/qms/buildings/annual-snapshot/download/{job.ReferenceYear}";
                await _downloadJobRepository.UpdateDownloadJob(job);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Annual snapshot export failed for job {JobId}, year {Year}.",
                    job.Id, job.ReferenceYear);

                job.Status = DownloadStatus.FAILED;
                job.CompletedAt = DateTime.UtcNow;
                await _downloadJobRepository.UpdateDownloadJob(job);

                throw new AppException("Annual snapshot export failed");
            }

        }

        /// <summary>
        /// Merr root path për eksport:
        /// 1) Export:RootPath nga konfigurimi (appsettings / env)
        /// 2) wwwroot/exports nëse ekziston WebRootPath
        /// 3) ContentRootPath/exports si fallback (p.sh. /app/exports në Docker)
        /// </summary>
        private string GetExportRoot()
        {
            // 1) from configuration
            var configured = _configuration["Export:RootPath"];
            if (!string.IsNullOrWhiteSpace(configured))
            {
                return configured;
            }

            // 2) if there is wwwroot
            if (!string.IsNullOrEmpty(_env.WebRootPath))
            {
                return Path.Combine(_env.WebRootPath, "exports");
            }

            // 3) fallback → app root
            return Path.Combine(_env.ContentRootPath, "exports");
        }

        private async Task WriteCsvFromStoredProcAsync(
            ZipArchive zip,
            string entryName,
            SqlConnection connection,
            string storedProcName,
            int referenceYear,
            int commandTimeoutSeconds)
        {
            var entry = zip.CreateEntry(entryName, CompressionLevel.Optimal);

            using var entryStream = entry.Open();
            using var writer = new StreamWriter(
                entryStream,
                new UTF8Encoding(encoderShouldEmitUTF8Identifier: false),
                bufferSize: 65536,
                leaveOpen: true);

            using var cmd = new SqlCommand(storedProcName, connection)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = commandTimeoutSeconds
            };

            cmd.Parameters.Add(new SqlParameter("@ReferenceYear", SqlDbType.Int) { Value = referenceYear });

            using var reader = await cmd.ExecuteReaderAsync(CommandBehavior.SequentialAccess);

            var fieldCount = reader.FieldCount;
            var headers = new string[fieldCount];

            for (int i = 0; i < fieldCount; i++)
            {
                headers[i] = reader.GetName(i);
            }

            await writer.WriteLineAsync(string.Join(";", headers));

            while (await reader.ReadAsync())
            {
                var values = new string[fieldCount];

                for (int i = 0; i < fieldCount; i++)
                {
                    var val = reader.GetValue(i);
                    values[i] = FormatCsvValue(val);
                }

                await writer.WriteLineAsync(string.Join(";", values));
            }

            await writer.FlushAsync();
        }

        private string FormatCsvValue(object val)
        {
            if (val == null || val == DBNull.Value)
                return string.Empty;

            var s = val.ToString() ?? string.Empty;

            // remove new lines
            s = s.Replace("\r", " ").Replace("\n", " ");

            // replace semicolon with comma
            s = s.Replace(";", ",");

            return s;
        }
    }
}
