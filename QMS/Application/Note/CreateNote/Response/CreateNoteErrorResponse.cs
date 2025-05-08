
namespace Application.Note.CreateNote.Response
{
    public class CreateNoteErrorResponse : CreateNoteResponse
    {
        public string? Message { get; set; }
        public string? Code { get; set; }
    }
}
