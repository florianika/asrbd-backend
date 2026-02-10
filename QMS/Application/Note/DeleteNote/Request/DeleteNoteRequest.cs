namespace Application.Note.DeleteNote.Request
{
    public class DeleteNoteRequest : Note.Request
    {
        public long Id { get; set; }
        public required string? Role { get; set; }//role of the user who wants to delete the comment
        public Guid UserId { get; set; }//userId of the user who wants to delete dhe comment
    }
}
