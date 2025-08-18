using Application.Queries.GetBuildingSummaryStats;

namespace Application.Queries.GetFieldworkProgressByMunicipality.Response
{
    public class GetFieldworkProgressByMunicipalityResponse
    {
        public List<FieldworkProgressByMunicipalityDTO> progressDTO { get; set; } = new();
    }
}
