namespace Application.Queries.GetDwellingQualityStats
{
    public class DwellingQualityStatsDTO
    {
        public string? Municipality { get; set; }
        public string? Quality { get; set; }
        public int TotalDwellings { get; set; }
    }
}
