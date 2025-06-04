
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

namespace Infrastructure.Repositories
{
    public class FieldWorkRepository : IFieldWorkRepository
    {
        private readonly DataContext _context;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public FieldWorkRepository(DataContext dataContext, IServiceScopeFactory serviceScopeFactory)
        {
            _context = dataContext;
            _serviceScopeFactory = serviceScopeFactory;
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

                await dbContext.Database.ExecuteSqlRawAsync(
                    @"exec UpdateBldReviewStatusToRequired  @FiledWorkId, @UpdatedUser", parameters.ToArray());

                return true; // Indicate success
            }
            catch (Exception ex)
            {
                throw new Exception("Error executing stored procedure.", ex);
            }
        }

        public async Task<List<UserDTO>> GetActiveUsers()
        {
            return await _context.Set<UserDTO>()
                .FromSqlRaw("EXEC [dbo].[GetActiveUsers]") // Call stored procedure that returns active users from QMS database
                .ToListAsync();
        }

    }
}
