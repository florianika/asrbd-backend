
namespace Application.Queries.GetFieldworkProgressByMunicipality
{
    public class FieldworkProgressByMunicipalityDTO
    {
        public int MunicipalityCode { get; set; }
        public string MunicipalityName { get; set; } = "";
        public int ApprovedBuildings { get; set; }
        public int FieldworkBuildings { get; set; }
        public decimal ProgressPercent { get; set; } // DECIMAL(5,2)
        public string Status { get; set; } = "";
    }
}
