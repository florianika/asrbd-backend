
using Application.Note.GetBuildingNotes.Request;
using Application.Note.GetBuildingNotes.Response;

namespace Application.Note.GetBuildingNotes
{
    public interface IGetBuildingNotes : INote<GetBuildingNotesRequest, GetBuildingNotesResponse>
    {
    }
}
