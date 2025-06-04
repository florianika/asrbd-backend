using Application.FieldWorkRule.GetStatisticsByRule.Request;
using Application.Queries.GetStatisticsFromRules;
using Application.Queries.GetStatisticsFromRules.Request;
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

        public async Task<GetStatisticsFromRulesResponse> Execute(GetStatisticsFromRulesRequest request)
        {
            var list = await _context.Set<RuleStatisticsDTO>()
                .FromSql($"EXEC [asrbd-qms].[dbo].[GetStatisticsFromRules]  @FieldWorkId = {request.Id}")
                .ToListAsync();

            return new GetStatisticsFromRulesResponse
            {
                Statistics = list
            };
        }
    }
}
