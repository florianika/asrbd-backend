
namespace Application.Ports
{
    public interface IFiledWorkRepository
    {
        Task<List<Domain.FieldWork>> GetAllFieldWork();
    }
}
