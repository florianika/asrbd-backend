namespace Application.Note
{
    public class NoteDTO
    {
        public long NoteId { get; set; }
        public Guid BldId { get; set; }
        public required string NoteText { get; set; }
        public required string CreatedUser { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public string? UpdatedUser { get; set; }
        public DateTime? UpdatedTimestamp { get; set; }
        
        public Guid UserId { get; set; }
    }
}
