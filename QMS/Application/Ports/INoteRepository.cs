namespace Application.Ports
{
    public interface INoteRepository
    {
        Task<long> CreateNote(Domain.Note note);
    }
}