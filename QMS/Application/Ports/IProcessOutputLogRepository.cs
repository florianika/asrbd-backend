using System;

namespace Application.Ports
{
    public interface IProcessOutputLogRepository
    {
        Task<Guid> CreateProcessOutputLog(Domain.ProcessOutputLog processOutputLog); 
        Task<List<Domain.ProcessOutputLog>> GetProcessOutputLogs(Guid buildingId);
        Task<Domain.ProcessOutputLog> GetProcessOutputLog(Guid id);
    }
}
