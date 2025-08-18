using Application.Queries.GetBuildingSummaryStats.Response;
using Application.Queries.GetBuildingSummaryStats;
using Application.Queries.GetFieldworkProgressByMunicipality;
using Application.Queries.GetFieldworkProgressByMunicipality.Response;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Queries.GetFieldworkProgressByMunicipality
{
    public class GetFieldworkProgressByMunicipalityQuery : IGetFieldworkProgressByMunicipalityQuery
    {
        private readonly DataContext _context;
        public GetFieldworkProgressByMunicipalityQuery(DataContext context)
        {
            _context = context;
        }
        public async Task<GetFieldworkProgressByMunicipalityResponse> Execute()
        {
            var list = await _context.Set<FieldworkProgressByMunicipalityDTO>()
                .FromSqlRaw("EXEC [asrbd-qms].[dbo].[FieldworkProgress_ByMunicipality_WithNames]")
                .ToListAsync();
            return new GetFieldworkProgressByMunicipalityResponse
            {
                progressDTO = list
            };
        }
    }
}
