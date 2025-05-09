
using Application.Rule;

namespace Application.Note.GetNote.Response
{
    public class GetNoteSuccessResponse : GetNoteResponse
    {
        public NoteDTO NotesDTO { get; set; }
    }
}
