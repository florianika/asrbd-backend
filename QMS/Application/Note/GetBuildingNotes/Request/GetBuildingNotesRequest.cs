
namespace Application.Note.GetBuildingNotes.Request
{
    public class GetBuildingNotesRequest : Note.Request
    {
        public Guid BldId { get; set; }
    }
}
