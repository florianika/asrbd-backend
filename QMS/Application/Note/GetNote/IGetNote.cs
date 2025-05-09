
using Application.Note.GetNote.Request;
using Application.Note.GetNote.Response;

namespace Application.Note.GetNote
{
    public interface IGetNote : INote<GetNoteRequest, GetNoteResponse>
    {
    }
}
