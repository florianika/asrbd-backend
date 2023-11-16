using Application.Ports;
using Domain;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class RuleRepository : IRuleRepository
    {
        private readonly DataContext _context;
        public RuleRepository(DataContext dataContext)
        {
            _context = dataContext;
        }
        public async Task<long> CreateRule(Rule rule)
        {
            await _context.Rules.AddAsync(rule);
            await _context.SaveChangesAsync();
            return rule.Id;
        }
    }
}
