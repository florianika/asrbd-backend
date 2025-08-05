using Application.Queries.GetBuildingSummaryStats;
using Application.Queries.GetStatisticsFromBuilding.Response;
using Application.Queries.GetStatisticsFromBuilding;
using Infrastructure.Context;
using Application.Queries.GetBuildingSummaryStats.Response;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Queries.GetBuildingSummaryStats
{
    public class GetBuildingSummaryStatsQuery : IGetBuildingSummaryStatsQuery
    {
        private readonly DataContext _context;

        public GetBuildingSummaryStatsQuery(DataContext context)
        {
            _context = context;
        }

        public async Task<GetBuildingSummaryStatsResponse> Execute()
        {
            var list = await _context.Set<BuildingSummaryStatsDTO>()
                .FromSqlRaw("EXEC [asrbd-qms].[dbo].[GetBuildingSummaryStats]")
                .ToListAsync();

            return new GetBuildingSummaryStatsResponse
            {
                statsDTO = list
            };
        }
    }
}
