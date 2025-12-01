using Application.Queries.GetBuildingQualityStats;
using Application.Queries.GetBuildingQualityStats.Response;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Queries.GetBuildingQualityStats
{
    public class GetBuildingQualityStatsQuery : IGetBuildingQualityStatsQuery
    {
        private readonly DataContext _context; 
        public GetBuildingQualityStatsQuery(DataContext context)
        {
            _context = context;
        }
        public async Task<GetBuildingQualityStatsResponse> Execute()
        {
            var list = await _context.Set<BuildingQualityStatsDTO>()
                .FromSqlRaw("EXEC [asrbd-qms].[dbo].[GetBuildingQualityStats]")
                .ToListAsync();
            return new GetBuildingQualityStatsResponse
            {
                statsDTO = list
            };
        }
    }
}
