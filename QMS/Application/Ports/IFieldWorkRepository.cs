
using Application.FieldWork.SendFieldWorkEmail;
using Domain.Enum;

namespace Application.Ports
{
    public interface IFieldWorkRepository
    {
        Task<List<Domain.FieldWork>> GetAllFieldWork();
        Task<int> CreateFieldWork(Domain.FieldWork fieldwork);
        Task<Domain.FieldWork> GetFieldWork(int id);
        Task<Domain.FieldWork> GetFieldWorkByIdAndStatus(int id, FieldWorkStatus status);
        Task UpdateFieldWork(Domain.FieldWork fieldwork);
        Task<Domain.FieldWork> GetActiveFieldWork();
        Task<bool>UpdateBldReviewStatus(int id, Guid updatedUser);
        Task<List<UserDTO>> GetActiveUsers();
    }
}
