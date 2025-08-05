
namespace Application.Queries.GetBuildingSummaryStats
{
    public class BuildingSummaryStatsDTO
    {
        public string? Municipality { get; set; }
        public string? Quality { get; set; }
        public string? Review { get; set; }
        public int TotalBuildings { get; set; }
    }
}
