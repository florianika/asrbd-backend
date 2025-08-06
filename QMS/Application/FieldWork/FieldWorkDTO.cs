
using Domain.Enum;

namespace Application.FieldWork
{
    public class FieldWorkDTO
    {
        public int FieldWorkId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public required string FieldWorkName { get; set; }
        public FieldWorkStatus fieldWorkStatus { get; set; }
        public string? Description { get; set; }
        public int OpenEmailTemplateId { get; set; }
        public int CloseEmailTemplateId { get; set; }
        public Guid CreatedUser { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public Guid? UpdatedUser { get; set; }
        public DateTime? UpdatedTimestamp { get; set; }
        public string? Remarks { get; set; }
    }
}
