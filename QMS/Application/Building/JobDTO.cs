using Domain.Enum;

namespace Application.Building
{
    public class JobDTO
    {
        public int Id { get; set; }
        public int? FieldWorkId { get; set; }
        public JobStatus Status { get; set; }
        public Guid CreatedUser { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public DateTime? CompletedTimestamp { get; set; }
    }
}
