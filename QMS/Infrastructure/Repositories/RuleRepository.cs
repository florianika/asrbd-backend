using Application.Exceptions;
using Application.Ports;
using Domain;
using Domain.Enum;
using Infrastructure.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Reflection.Metadata;

namespace Infrastructure.Repositories
{
    public class RuleRepository : IRuleRepository
    {
        private readonly DataContext _context;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public RuleRepository(DataContext dataContext, IServiceScopeFactory serviceScopeFactory)
        {
            _context = dataContext;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task ChangeRuleStatus(long id, RuleStatus status, Guid updatedUser)
        {
            var rule = await _context.Rules.FirstOrDefaultAsync(u => u.Id == id)
                           ?? throw new NotFoundException("Rule not found");
            rule.RuleStatus = status;
            rule.UpdatedUser = updatedUser;
            rule.UpdatedTimestamp = DateTime.Now;
            _context.SaveChanges();
        }

        public async Task<long> CreateRule(Domain.Rule rule)
        {
            await _context.Rules.AddAsync(rule);
            await _context.SaveChangesAsync();
            return rule.Id;
        }

        public async Task<List<Domain.Rule>> GetAllRules()
        {
            return await _context.Rules.ToListAsync();
        }

        public async Task<Domain.Rule> GetRule(long id)
        {
            return await _context.Rules.FirstOrDefaultAsync(x => x.Id.Equals(id))
                ?? throw new NotFoundException("Rule not found");
        }

        public async Task<List<Domain.Rule>> GetRulesByEntity(EntityType entityType)
        {
            return await _context.Rules.Where(x => x.EntityType == entityType).ToListAsync();
        }

        public async Task<List<Domain.Rule>> GetActiveRulesByEntity(EntityType entityType) 
        {
            return await _context.Rules.Where(x => x.EntityType == entityType && x.RuleStatus == RuleStatus.ACTIVE).ToListAsync();
        }

        public async Task<List<Domain.Rule>> GetRulesByQualityAction(QualityAction qualityAction)
        {
            return await _context.Rules.Where(x => x.QualityAction == qualityAction).ToListAsync();
        }

        public async Task<List<Domain.Rule>> GetRulesByVariableAndEntity(string variable, EntityType entityType)
        {
            return await _context.Rules.Where(x => x.Variable == variable
                                        && x.EntityType == entityType).ToListAsync();
        }
        
        public async Task UpdateRule(Domain.Rule rule)
        {
            _context.Rules.Update(rule);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> ExecuteRulesStoreProcedure(List<Guid> buildingIds, Guid CreatedUser)
        {
            try
            {
                string BldIds = "";
                if (buildingIds.Count > 0)
                {
                    foreach (Guid guid in buildingIds)
                    {
                        BldIds += "'" + guid.ToString() + "',";
                    }
                    BldIds = BldIds.Remove(BldIds.Length - 1, 1);
                }
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@buildingIds", BldIds));
                parameters.Add(new SqlParameter("@CreatedUser", CreatedUser));

                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();

                    await dbContext.Database.ExecuteSqlRawAsync(
                        @"exec ExecuteRulesStoredProcedure @buildingIds, @CreatedUser", parameters.ToArray());

                    return true; // Indicate success
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error executing stored procedure.", ex);
            }
        }

        public async Task<bool> UpdateEntitiesStoredProcedure(List<Guid> buildingIds, Guid CreatedUser)
        {

            try
            {
                string BldIds = "";
                if (buildingIds.Count > 0)
                {
                    foreach (Guid guid in buildingIds)
                    {
                        BldIds += "'" + guid.ToString() + "',";
                    }
                    BldIds = BldIds.Remove(BldIds.Length - 1, 1);
                }
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@buildingIds", BldIds));
                parameters.Add(new SqlParameter("@CreatedUser", CreatedUser));

                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();

                    await dbContext.Database.ExecuteSqlRawAsync(
                        @"exec UpdateEntitiesStoredProcedure @buildingIds, @CreatedUser", parameters.ToArray());

                    return true; // Indicate success
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error executing stored procedure.", ex);
            }
        }
    }
}
