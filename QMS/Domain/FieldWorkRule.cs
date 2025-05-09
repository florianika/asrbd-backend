namespace Domain
{
    public class FieldWorkRule
    {
        public long Id { get; set; }
        public int FieldWorkId { get; set; }
        public long RuleId { get; set; }
        public Guid CreatedUser { get; set; }
        public DateTime CreatedTimestamp { get; set; }
    }
}
