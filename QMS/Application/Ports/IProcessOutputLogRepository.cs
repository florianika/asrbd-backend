
namespace Application.Ports
{
    public interface IProcessOutputLogRepository
    {
        Task<Guid> CreateProcessOutputLog(Domain.ProcessOutputLog processOutputLog); 
        Task<List<Domain.ProcessOutputLog>> GetProcessOutputLogsByBuildingId(Guid buildingId);

        Task<List<Domain.ProcessOutputLog>> GetProcessOutputLogsByEntranceId(Guid entranceId);
        Task<List<Domain.ProcessOutputLog>> GetProcessOutputLogsByDwellingId(Guid dwellingId);
        Task<Domain.ProcessOutputLog> GetProcessOutputLog(Guid id);
    }
}
