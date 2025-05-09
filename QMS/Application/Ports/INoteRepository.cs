namespace Application.Ports
{
    public interface INoteRepository
    {
        Task<long> CreateNote(Domain.Note note);
        Task<List<Domain.Note>> GetBuildingNotes(Guid buildingId);
        Task<Domain.Note> GetNote(long id);
        Task UpdateNote(Domain.Note note);
        Task DeleteNote(Domain.Note note);
    }
}