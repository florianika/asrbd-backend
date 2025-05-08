namespace Application.Note.GetBuildingNotes.Response
{
    public class GetBuildingNotesSuccessResponse : GetBuildingNotesResponse
    {
        public IEnumerable<NoteDTO> NotesDTO { get; set; }
    }
}
