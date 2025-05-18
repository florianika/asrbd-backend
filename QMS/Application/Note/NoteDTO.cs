namespace Application.Note
{
    public class NoteDTO
    {
        public long NoteId { get; set; }
        public Guid BldId { get; set; }
        //TODO check if this should be declared as required
        public string NoteText { get; set; }
        public Guid CreatedUser { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public Guid? UpdatedUser { get; set; }
        public DateTime? UpdatedTimestamp { get; set; }
    }
}
