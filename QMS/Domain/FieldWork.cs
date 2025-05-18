
using Domain.Enum;

namespace Domain
{
    public class FieldWork
    {
        public int FieldWorkId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public FieldWorkStatus FieldWorkStatus { get; set; }
        public string? Description { get; set; }
        public int EmailTemplateId { get; set; }
        public Guid CreatedUser { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public Guid? UpdatedUser { get; set; }
        public DateTime? UpdatedTimestamp { get; set; }
        public string? Remarks { get; set; }
        public ICollection<FieldWorkRule> FieldWorkRules { get; set; } = new List<FieldWorkRule>();
    }
}
