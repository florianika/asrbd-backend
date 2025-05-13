using Application.Queries.GetStatisticsFromRules;
using Application.Queries.GetStatisticsFromRules.Response;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Queries.GetStatisticsFromRules
{
    public class GetStatisticsFromRulesQuery : IGetStatisticsFromRulesQuery
    {
        private readonly DataContext _context;

        public GetStatisticsFromRulesQuery(DataContext context)
        {
            _context = context;
        }

        public async Task<GetStatisticsFromRulesResponse> Execute()
        {
            var list = await _context.Set<RuleStatisticsDTO>()
                .FromSqlRaw("EXEC [asrbd-qms].[dbo].[GetStatisticsFromRules]")
                .ToListAsync();

            return new GetStatisticsFromRulesResponse
            {
                Statistics = list
            };
        }
    }
}
