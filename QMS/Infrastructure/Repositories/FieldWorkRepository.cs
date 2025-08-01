
using Application.Exceptions;
using Application.Ports;
using Domain;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using Domain.Enum;
using Microsoft.Data.SqlClient;
using Application.FieldWork.SendFieldWorkEmail;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories
{
    public class FieldWorkRepository : IFieldWorkRepository
    {
        private readonly DataContext _context;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger _logger;
        public FieldWorkRepository(DataContext dataContext, IServiceScopeFactory serviceScopeFactory, ILogger<FieldWorkRepository> logger)
        {
            _context = dataContext;
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }
        public async Task<List<FieldWork>> GetAllFieldWork()
        {
            return await _context.FieldWorks.ToListAsync();
        }
        public async Task<int> CreateFieldWork(Domain.FieldWork fieldwork)
        {
            await _context.FieldWorks.AddAsync(fieldwork);
            await _context.SaveChangesAsync();
            return fieldwork.FieldWorkId;
        }
        public async Task<FieldWork> GetFieldWork(int id)
        {
            return await _context.FieldWorks.FirstOrDefaultAsync(x => x.FieldWorkId.Equals(id))
                ?? throw new NotFoundException("FieldWork not found");
        }
        
        public async Task<FieldWork> GetFieldWorkByIdAndStatus(int id, FieldWorkStatus status)
        {
            return await _context.FieldWorks.FirstOrDefaultAsync(x => x.FieldWorkId.Equals(id) 
                                                                      && x.FieldWorkStatus == status)
                   ?? throw new NotFoundException("FieldWork with status NEW not found");
        }
        public async Task UpdateFieldWork(FieldWork fieldwork)
        {
            _context.FieldWorks.Update(fieldwork);
            await _context.SaveChangesAsync();
        }

        public async Task<FieldWork> GetActiveFieldWork()
        {
            return await _context.FieldWorks.FirstOrDefaultAsync(x => x.FieldWorkStatus != Domain.Enum.FieldWorkStatus.CLOSED)
                ?? throw new NotFoundException("FieldWork not found");
        }

        public async Task<FieldWork> GetCurrentOpenFieldwork()
        {
            return await _context.FieldWorks
            .Where(f => f.FieldWorkStatus == Domain.Enum.FieldWorkStatus.OPEN)
            .OrderByDescending(f => f.StartDate)
            .FirstOrDefaultAsync();
        }
        public async Task<bool> HasActiveFieldWork()
        {
            // Check if there are records that do not have status CLOSED, → if yes, return FALSE
            var hasActive = await _context.FieldWorks
                .AnyAsync(x => x.FieldWorkStatus != Domain.Enum.FieldWorkStatus.CLOSED);

            return !hasActive;
        }

        public async Task<bool> UpdateBldReviewStatus(int id, Guid updatedUser)
        {
            try
            {
                
                var parameters = new List<SqlParameter>
                {
                    new ("@FiledWorkId", id),
                    new ("@UpdatedUser", updatedUser)
                };

                using var scope = _serviceScopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();

                dbContext.Database.SetCommandTimeout(5000);
                await dbContext.Database.ExecuteSqlRawAsync(
                    @"exec UpdateBldReviewStatusToRequired  @FiledWorkId, @UpdatedUser", parameters.ToArray());

                return true; // Indicate success
            }
            catch (SqlException sqlEx)
            {
                var sqlError = sqlEx.Errors.Cast<SqlError>().FirstOrDefault();
                var procedure = sqlError?.Procedure ?? "unknown procedure";
                var line = sqlError?.LineNumber.ToString() ?? "unknown line";
                var errorNumber = sqlError?.Number.ToString() ?? "N/A";

                var detailedMessage = $"SQL error (#{errorNumber}) in procedure '{procedure}' at line {line}: {sqlEx.Message}";
                _logger.LogError(sqlEx, "ExecuteRulesStoreProcedure failed: {Error}", detailedMessage);

                throw new AppException(detailedMessage, sqlEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error executing ExecuteRules");
                throw new AppException("Unexpected error occurred while executing rules.", ex);
            }
        }

        public async Task<List<UserDTO>> GetActiveUsers()
        {
            return await _context.Set<UserDTO>()
                .FromSqlRaw("EXEC [dbo].[GetActiveUsers]") // Call stored procedure that returns active users from QMS database
                .ToListAsync();
        }

        public async Task<int> CreateJob(Domain.Jobs job)
        {
            await _context.Jobs.AddAsync(job);
            await _context.SaveChangesAsync();
            return job.Id;
        }

        public async Task<Jobs> GetJobById(int id)
        {
            return await _context.Jobs.FirstOrDefaultAsync(x => x.Id.Equals(id))
                ?? throw new NotFoundException("Job not found");
        }
        public async Task UpdateJob(Domain.Jobs job)
        {
            _context.Jobs.Update(job);
            await _context.SaveChangesAsync();
        }

        public async Task ExecuteStatisticsSP(int jobId)
        {
            try
            {
                var parameters = new List<SqlParameter>
                {
                    new ("@JobId", jobId)
                };

                using var scope = _serviceScopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
                dbContext.Database.SetCommandTimeout(5000);
                await dbContext.Database.ExecuteSqlRawAsync(
                    @"exec ExecuteStatistics  @JobId", parameters.ToArray());
            }
            catch (SqlException sqlEx)
            {
                var sqlError = sqlEx.Errors.Cast<SqlError>().FirstOrDefault();
                var procedure = sqlError?.Procedure ?? "unknown procedure";
                var line = sqlError?.LineNumber.ToString() ?? "unknown line";
                var errorNumber = sqlError?.Number.ToString() ?? "N/A";

                var detailedMessage = $"SQL error (#{errorNumber}) in procedure '{procedure}' at line {line}: {sqlEx.Message}";
                _logger.LogError(sqlEx, "ExecuteStatistics failed: {Error}", detailedMessage);

                throw new AppException(detailedMessage, sqlEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error executing ExecuteRules");
                throw new AppException("Unexpected error occurred while executing rules.", ex);
            }
        }

        public async Task<Jobs> GetJob(int id)
        {
            return await _context.Jobs.FirstOrDefaultAsync(x => x.Id.Equals(id))
               ?? throw new NotFoundException("Job not found");
        }

        public async Task<List<Statistics>> GetStatistics(int id)
        {
            return await _context.Statistics.Where(s => s.JobId.Equals(id)).OrderBy(s => s.Municipality).ToListAsync();
        }

        public async Task ExecuteTestUntestedBldSP(int jobId)
        {
            try
            {
                var parameters = new List<SqlParameter>
                {
                    new ("@JobId", jobId)
                };

                using var scope = _serviceScopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
                dbContext.Database.SetCommandTimeout(5000);
                await dbContext.Database.ExecuteSqlRawAsync(
                    @"exec TestUntestedBld  @JobId", parameters.ToArray());
            }
            catch (SqlException sqlEx)
            {
                var sqlError = sqlEx.Errors.Cast<SqlError>().FirstOrDefault();
                var procedure = sqlError?.Procedure ?? "unknown procedure";
                var line = sqlError?.LineNumber.ToString() ?? "unknown line";
                var errorNumber = sqlError?.Number.ToString() ?? "N/A";

                var detailedMessage = $"SQL error (#{errorNumber}) in procedure '{procedure}' at line {line}: {sqlEx.Message}";
                _logger.LogError(sqlEx, "TestUntestedBld failed: {Error}", detailedMessage);

                throw new AppException(detailedMessage, sqlEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error executing TestUntestedBld");
                throw new AppException("Unexpected error occurred while executing rules.", ex);
            }
        }
    }
}
