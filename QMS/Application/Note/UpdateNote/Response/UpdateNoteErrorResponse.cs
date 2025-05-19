
namespace Application.Note.UpdateNote.Response
{
    public class UpdateNoteErrorResponse : UpdateNoteResponse
    {
        public required string Message { get; set; } 
        public required string Code { get; set; }
    }
}
