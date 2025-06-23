
using Application.Exceptions;
using Application.Ports;
using Domain;
using Domain.Enum;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Infrastructure.Repositories
{
    public class FieldWorkRuleRepository : IFieldWorkRuleRepository
    {
        private readonly DataContext _context;
        public FieldWorkRuleRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<long> AddFieldWorkRule(FieldWorkRule fieldWorkRule)
        {
            await _context.FieldWorkRules.AddAsync(fieldWorkRule);
            await _context.SaveChangesAsync();
            return fieldWorkRule.Id;
        }

        public async Task<FieldWorkRule> GetFieldWorkRule(int id, long ruleId)
        {
            return await _context.FieldWorkRules.FirstOrDefaultAsync(x => x.RuleId.Equals(ruleId) && x.FieldWorkId.Equals(id))
               ?? throw new NotFoundException($"FieldWorkRule with FieldWorkId={id} and RuleId={ruleId} not found");
        }

        public async Task<bool> ExistsRule(int fieldWorkId, long ruleId)
        {
            return await _context.FieldWorkRules
                .AnyAsync(x => x.FieldWorkId == fieldWorkId && x.RuleId == ruleId);
        }
        public async Task RemoveFieldWorkRule(FieldWorkRule fieldWorkRule)
        {
            _context.FieldWorkRules.Remove(fieldWorkRule);
            await _context.SaveChangesAsync();
        }
        public async Task<List<FieldWorkRule>> GetRuleByField(int fieldWorkId)
        {
            return await _context.FieldWorkRules.Where(r => r.FieldWorkId == fieldWorkId).ToListAsync();
        }

        public async Task<long> GetStatisticsByRule(long id)
        {
            return await _context.ProcessOutputLogs
            .Where(pol => pol.RuleId == id)
            .Select(pol => pol.BldId)
            .Distinct()
            .CountAsync();
        }

        public async Task<List<FieldWorkRule>> GetRuleByFieldWorkAndEntity(int id, EntityType entityType)
        {
            return await _context.FieldWorkRules
            .Include(r => r.Rule) 
            .Where(r => r.FieldWorkId == id && r.Rule.EntityType == entityType)
            .ToListAsync();
        }
    }
}
