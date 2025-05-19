namespace Application.Note.CreateNote.Request
{
    public class CreateNoteRequest : Note.Request
    {
        public Guid BldId { get; set; }
        public required string NoteText { get; set; }
        public Guid CreatedUser { get; set; }
    }
}
