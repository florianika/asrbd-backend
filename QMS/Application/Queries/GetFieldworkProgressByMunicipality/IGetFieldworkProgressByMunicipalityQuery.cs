using Application.Queries.GetFieldworkProgressByMunicipality.Response;

namespace Application.Queries.GetFieldworkProgressByMunicipality
{
    public interface IGetFieldworkProgressByMunicipalityQuery
    {
        Task<GetFieldworkProgressByMunicipalityResponse> Execute();
    }
}
