
namespace Domain
{
    #nullable disable
    public class Note
    {
        public long NoteId { get; set; }
        public Guid BldId { get; set; }
        public string NoteText { get; set; }
        public string CreatedUser { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public string? UpdatedUser { get; set; }
        public DateTime? UpdatedTimestamp { get; set; }
        public Guid UserId { get; set; }

    }
}
