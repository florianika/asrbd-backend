namespace Application.Note.GetBuildingNotes.Response
{
    public class GetBuildingNotesSuccessResponse : GetBuildingNotesResponse
    {
        public required IEnumerable<NoteDTO> NotesDTO { get; set; }
    }
}
