namespace Application.Note.GetBuildingNotes.Response
{
    public class GetBuildingNotesErrorResponse : GetBuildingNotesResponse
    {
        public string? Message { get; set; }
        public string? Code { get; set; }
    }
}
