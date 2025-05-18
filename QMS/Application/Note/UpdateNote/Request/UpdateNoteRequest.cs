namespace Application.Note.UpdateNote.Request
{
    public class UpdateNoteRequest : Note.Request
    {
        public long NoteId { get; set; }
        //TODO check if this should be declared as required
        public string NoteText { get; set; }
        public Guid UpdatedUser { get; set; }
    }
}
