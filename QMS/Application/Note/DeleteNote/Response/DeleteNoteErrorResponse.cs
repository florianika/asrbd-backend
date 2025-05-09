namespace Application.Note.DeleteNote.Response
{
    public class DeleteNoteErrorResponse : DeleteNoteResponse
    {
        public string Message { get; internal set; }
        public string Code { get; internal set; }
    }
}
