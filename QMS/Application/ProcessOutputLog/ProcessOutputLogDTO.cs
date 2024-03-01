using Domain.Enum;
namespace Application.ProcessOutputLog
{
    public class ProcessOutputLogDTO
    {
        public Guid Id { get; set; }
        public long RuleId { get; set; }
        public Guid BldId { get; set; }
        public Guid? EntId { get; set; }
        public Guid? DwlId { get; set; }
        public EntityType EntityType { get; set; }
        public string? Variable { get; set; }
        public QualityAction QualityAction { get; set; }
        public QualityStatus QualityStatus { get; set; }
        public string? QualityMessageAl { get; set; }
        public string? QualityMessageEn { get; set; }
        public ErrorLevel ErrorLevel { get; set; }
        public Guid CreatedUser { get; set; }
        public DateTime CreatedTimestamp { get; set; }
    }
}
