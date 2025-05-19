
using Application.Rule;

namespace Application.Note.GetNote.Response
{
    public class GetNoteSuccessResponse : GetNoteResponse
    {
        public required NoteDTO NotesDTO { get; set; }
    }
}
