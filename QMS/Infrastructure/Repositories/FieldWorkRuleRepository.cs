
using Application.Exceptions;
using Application.Ports;
using Domain;
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

        public async Task<FieldWorkRule> GetFieldWorkRule(long id)
        {
            return await _context.FieldWorkRules.FirstOrDefaultAsync(x => x.Id.Equals(id))
               ?? throw new NotFoundException("FieldWorkRule not found");
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
    }
}
