namespace Application.Queries.GetBuildingQualityStats
{
    public class BuildingQualityStatsDTO
    {
        public string? Municipality { get; set; }
        public string? Quality { get; set; }
        public int TotalBuildings { get; set; }
    }
}
