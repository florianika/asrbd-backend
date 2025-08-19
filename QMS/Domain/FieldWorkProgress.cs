namespace Domain
{
    public class FieldWorkProgress
    {
        public int Id { get; set; }
        public int FieldworkId { get; set; }
        public int MunicipalityCode { get; set; } 
        public int ApprovedBuildings { get; set; }
        public int FieldworkBuildings  { get; set; }
        public decimal ProgressPercent { get; set; } 
    }
}
