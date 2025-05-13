
namespace Application.Queries.GetStatisticsFromBuilding
{
    public class BuildingStatisticsDTO
    {
        public short MunicipalityCode { get; set; }
        public string MunicipalityName { get; set; } = string.Empty;
        public int UniqueBldCount { get; set; }
    }
}
