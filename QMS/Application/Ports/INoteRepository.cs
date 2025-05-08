namespace Application.Ports
{
    public interface INoteRepository
    {
        Task<long> CreateNote(Domain.Note note);
        Task<List<Domain.Note>> GetBuildingNotes(Guid buildingId);
    }
}