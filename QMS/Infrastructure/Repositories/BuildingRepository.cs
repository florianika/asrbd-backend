using Application.Exceptions;
using Application.Ports;
using Domain;
using Infrastructure.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories
{
    public class BuildingRepository : IBuildingRepository
    {
        private readonly DataContext _context;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger _logger;
        public BuildingRepository(DataContext context,
            IServiceScopeFactory serviceScopeFactory,
            ILogger<BuildingRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<int> CreateJob(Domain.Jobs job)
        {
            await _context.Jobs.AddAsync(job);
            await _context.SaveChangesAsync();
            return job.Id;
        }

        public async Task ExecuteTestBuildingSP(int jobId, bool isAllBuildings)
        {
            try
            {
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@JobId", jobId),
                    new SqlParameter("@IsAllBuildings", isAllBuildings)
                };

                using var scope = _serviceScopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
                dbContext.Database.SetCommandTimeout(50000);
                await dbContext.Database.ExecuteSqlRawAsync(
                    @"EXEC dbo.TestBuildings @JobId, @IsAllBuildings", parameters.ToArray());
            }
            catch (SqlException sqlEx)
            {
                var sqlError = sqlEx.Errors.Cast<SqlError>().FirstOrDefault();
                var procedure = sqlError?.Procedure ?? "unknown procedure";
                var line = sqlError?.LineNumber.ToString() ?? "unknown line";
                var errorNumber = sqlError?.Number.ToString() ?? "N/A";

                var detailedMessage = $"SQL error (#{errorNumber}) in procedure '{procedure}' at line {line}: {sqlEx.Message}";
                _logger.LogError(sqlEx, "TestBuildings failed: {Error}", detailedMessage);

                throw new AppException(detailedMessage, sqlEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error executing TestBuildings");
                throw new AppException("Unexpected error occurred while executing TestBuildings.", ex);
            }
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
    }
}
