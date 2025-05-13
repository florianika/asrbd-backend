namespace Application.Queries.GetStatisticsFromRules
{
    public class RuleStatisticsDTO
    {
        public short MunicipalityCode { get; set; }
        public string MunicipalityName { get; set; } = string.Empty;
        public int UniqueBldCount { get; set; }
    }
}
