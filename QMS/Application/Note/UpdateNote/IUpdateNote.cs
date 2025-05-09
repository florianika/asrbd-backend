
using Application.Note.UpdateNote.Request;
using Application.Note.UpdateNote.Response;

namespace Application.Note.UpdateNote
{
    public interface IUpdateNote : INote<UpdateNoteRequest,UpdateNoteResponse>
    {
    }
}
