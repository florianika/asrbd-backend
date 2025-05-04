
namespace Application.Ports
{
    public interface IFieldWorkRepository
    {
        Task<List<Domain.FieldWork>> GetAllFieldWork();
        Task<int> CreateFieldWork(Domain.FieldWork fieldwork);
    }
}
