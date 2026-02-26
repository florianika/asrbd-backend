using Domain.Enum;

namespace Domain
{
    public class Jobs
    {
        public int Id { get; set; }
        public int? FieldWorkId { get; set; }
        public FieldWork? FieldWork { get; set; }
        public JobStatus Status { get; set; }
        public Guid CreatedUser { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public DateTime? CompletedTimestamp { get; set; }
    }
}
