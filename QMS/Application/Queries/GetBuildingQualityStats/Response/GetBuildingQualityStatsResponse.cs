namespace Application.Queries.GetBuildingQualityStats.Response
{
    public class GetBuildingQualityStatsResponse
    {
        public List<BuildingQualityStatsDTO> statsDTO { get; set; } = new();
    }
}
