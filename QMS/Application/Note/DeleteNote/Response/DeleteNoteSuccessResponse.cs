namespace Application.Note.DeleteNote.Response
{
    public class DeleteNoteSuccessResponse : DeleteNoteResponse
    {
        //TODO check if this should be declared as required
        public string Message { get; set; }
    }
}
