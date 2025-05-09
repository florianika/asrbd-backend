
namespace Application.Note.UpdateNote.Response
{
    public class UpdateNoteErrorResponse : UpdateNoteResponse
    {
        public string Message { get; internal set; }
        public string Code { get; internal set; }
    }
}
