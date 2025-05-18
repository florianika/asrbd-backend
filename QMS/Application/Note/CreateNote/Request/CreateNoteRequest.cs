namespace Application.Note.CreateNote.Request
{
    public class CreateNoteRequest : Note.Request
    {
        public Guid BldId { get; set; }
        //TODO check if this should be declared as required
        public string NoteText { get; set; }
        public Guid CreatedUser { get; set; }
    }
}
