namespace Application.Note.DeleteNote.Response
{
    public class DeleteNoteErrorResponse : DeleteNoteResponse
    {
        public required string Message { get; set; } 
        public required string Code { get; set; } 
    }
}
