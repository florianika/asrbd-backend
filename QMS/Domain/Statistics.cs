namespace Domain
{
    public class Statistics
    {
        public long Id { get; set; }
        public int JobId {  get; set; }
        public required Jobs Job { get; set; }
        public string? Municipality { get; set; }
        public int RuleStatistics { get; set; }
        public int BldStatistics { get; set; }

    }
}
