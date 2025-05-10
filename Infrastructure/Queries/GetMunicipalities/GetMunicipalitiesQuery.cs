using Application.Queries;
using Application.Queries.GetMunicipalities;
using Application.Queries.GetMunicipalities.Response;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Queries.GetMunicipalities
{
    public class GetMunicipalitiesQuery : IGetMunicipalitiesQuery
    {
        private readonly DataContext _context;

        public GetMunicipalitiesQuery(DataContext context)
        {
            _context = context;
        }

        public async Task<GetMunicipalitiesResponse> Execute()
        {
            var list = await _context.Set<MunicipalityDto>()
                .FromSqlRaw("EXEC [asrbd-qms].[dbo].[GetMunicipalitiesFromGDBItems]")
                .ToListAsync();

            return new GetMunicipalitiesResponse
            {
                Municipalities = list
            };
        }
    }
}
