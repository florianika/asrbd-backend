using Domain.Enum;

namespace Application.Building
{
    public class DownloadJobDTO
    {
        public int Id { get; set; }
        public int ReferenceYear { get; set; }
        public DownloadStatus Status { get; set; }
        public string? FileUrl { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public string? Remarks { get; set; }
        public Guid? LastUpdatedBy { get; set; }
    }
}
