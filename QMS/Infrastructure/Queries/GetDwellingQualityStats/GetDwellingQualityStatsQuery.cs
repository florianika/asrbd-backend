using Application.Queries.GetDwellingQualityStats;
using Application.Queries.GetDwellingQualityStats.Response;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Queries.GetDwellingQualityStats
{
    public class GetDwellingQualityStatsQuery : IGetDwellingQualityStatsQuery
    {
        private readonly DataContext _context;
        public GetDwellingQualityStatsQuery(DataContext context)
        {
            _context = context;
        }
        public async Task<GetDwellingQualityStatsResponse> Execute()
        {
            var list = await _context.Set<DwellingQualityStatsDTO>()
                .FromSqlRaw("EXEC [asrbd-qms].[dbo].[GetDwellingQualityStats]")
                .ToListAsync();
            return new GetDwellingQualityStatsResponse
            {
                statsDTO = list
            };
        }
    }
}
