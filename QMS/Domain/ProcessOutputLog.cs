
using Domain.Enum;

#nullable disable
namespace Domain
{
    public class ProcessOutputLog
    {
        public Guid Id { get; set; }
        // Foreign Key
        public long RuleId { get; set; }
        // Navigation Property
        public Rule Rule { get; set; }
        public long BldId { get; set; }
        public long? EntId { get; set; }
        public long? DwlId { get; set; }
        public EntityType EntityType { get; set; }
        public string Variable { get; set; }
        public QualityAction QualityAction { get; set; }
        public QualityStatus QualityStatus { get; set; }
        public string QualityMessageAl { get; set; }
        public string QualityMessageEn { get; set; }
        public ErrorLevel ErrorLevel { get; set; }
        public Guid CreatedUser { get; set; }
        public DateTime CreatedTimestamp { get; set; }
    }
}
