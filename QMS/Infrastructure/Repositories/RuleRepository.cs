using Application.Exceptions;
using Application.Ports;
using Application.Rule;
using Domain;
using Domain.Enum;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RuleRepository : IRuleRepository
    {
        private readonly DataContext _context;
        public RuleRepository(DataContext dataContext)
        {
            _context = dataContext;
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

        public async Task<long> CreateRule(Rule rule)
        {
            await _context.Rules.AddAsync(rule);
            await _context.SaveChangesAsync();
            return rule.Id;
        }
        public async Task<List<Rule>> GetAllRules()
        {
            return await _context.Rules.ToListAsync();
        }

        public async Task<Rule> GetRule(long id)
        {
            return await _context.Rules.FirstOrDefaultAsync(x => x.Id.Equals(id))
                ?? throw new NotFoundException("Rule not found"); ;
        }

        public async Task<List<Rule>> GetRulesByEntity(EntityType entityType)
        {
            return await _context.Rules.Where(x => x.EntityType == entityType).ToListAsync();
        }

        public async Task<List<Rule>> GetRulesByQualityAction(QualityAction qualityAction)
        {
            return await _context.Rules.Where(x => x.QualityAction == qualityAction).ToListAsync();
        }

        public async Task<List<Rule>> GetRulesByVariableAndEntity(string variable, EntityType entityType)
        {
            return await _context.Rules.Where(x => x.Variable== variable
                                        && x.EntityType == entityType).ToListAsync();
        }
        public async Task UpdateRule(Rule rule)
        {
            _context.Rules.Update(rule);
            await _context.SaveChangesAsync();
        }

    }
}
