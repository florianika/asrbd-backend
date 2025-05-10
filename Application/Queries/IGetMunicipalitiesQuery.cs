
using Application.Queries.GetMunicipalities;
using Application.Queries.GetMunicipalities.Response;

namespace Application.Queries
{
    public interface IGetMunicipalitiesQuery
    {
        Task<GetMunicipalitiesResponse> Execute();
    }
}
