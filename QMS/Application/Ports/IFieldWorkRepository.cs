
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
        Task<bool> HasActiveFieldWork();
        Task<int> CreateJob(Domain.Jobs job);
        Task<Domain.Jobs> GetJobById(int id);
        Task UpdateJob(Domain.Jobs job);
        Task ExecuteStatisticsSP(int jobId);
        Task<Domain.Jobs> GetJob(int id);
        Task<List<Domain.Statistics>> GetStatistics(int id);
        Task<Domain.FieldWork> GetCurrentOpenFieldwork();
        Task ExecuteTestUntestedBldSP(int jobId);
        Task<int> HasBldReviewExecuted();
    }
}
