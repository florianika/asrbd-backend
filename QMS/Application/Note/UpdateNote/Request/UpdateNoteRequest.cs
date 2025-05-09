namespace Application.Note.UpdateNote.Request
{
    public class UpdateNoteRequest : Note.Request
    {
        public long NoteId { get; set; }
        public string NoteText { get; set; }
        public Guid UpdatedUser { get; set; }
    }
}
