
namespace Application.Note.GetNote.Response
{
    public class GetNoteErrorResponse : GetNoteResponse
    {
        public string? Message { get; set; }
        public string? Code { get; set; }
    }
}
