
using Application.Note.CreateNote.Request;
using Application.Note.CreateNote.Response;

namespace Application.Note.CreateNote
{
    public interface ICreateNote : INote<CreateNoteRequest, CreateNoteResponse>
    {
    }
}
