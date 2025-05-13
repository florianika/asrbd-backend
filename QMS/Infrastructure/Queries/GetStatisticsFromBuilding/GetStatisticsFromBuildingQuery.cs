
using Application.Queries.GetStatisticsFromBuilding;
using Application.Queries.GetStatisticsFromBuilding.Response;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Queries.GetStatisticsFromBuilding
{
    public class GetStatisticsFromBuildingQuery : IGetStatisticsFromBuildingQuery
    {
        private readonly DataContext _context;

        public GetStatisticsFromBuildingQuery(DataContext context)
        {
            _context = context;
        }

        public async Task<GetStatisticsFromBuildingResponse> Execute()
        {
            var list = await _context.Set<BuildingStatisticsDTO>()
                .FromSqlRaw("EXEC [asrbd-qms].[dbo].[GetStatisticsFromBuildings]")
                .ToListAsync();

            return new GetStatisticsFromBuildingResponse
            {
                Statistics = list
            };
        }
    }
}
