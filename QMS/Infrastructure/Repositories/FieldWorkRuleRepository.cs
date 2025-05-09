
using Application.Ports;
using Domain;
using Infrastructure.Context;

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
    }
}
