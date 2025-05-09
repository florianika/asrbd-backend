
using Application.Note.DeleteNote.Request;
using Application.Note.DeleteNote.Response;

namespace Application.Note.DeleteNote
{
    public interface IDeleteNote : INote<DeleteNoteRequest, DeleteNoteResponse>
    {
    }
}
