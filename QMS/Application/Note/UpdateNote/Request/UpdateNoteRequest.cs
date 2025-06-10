namespace Application.Note.UpdateNote.Request
{
    public class UpdateNoteRequest : Note.Request
    {
        public long NoteId { get; set; }
        public required string NoteText { get; set; }
        public required string UpdatedUser { get; set; }
    }
}
