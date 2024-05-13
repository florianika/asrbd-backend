using System.Diagnostics.Contracts;
using System.Linq.Dynamic.Core;
using Application.Exceptions;
using Application.Ports;
using Domain;
using Domain.Enum;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Infrastructure.Repositories
{
    public class ProcessOutputLogRepository : IProcessOutputLogRepository
    {
        private readonly DataContext _context;
        
        public ProcessOutputLogRepository(DataContext context) 
        {
            _context = context;
        }

        public async Task<Guid> CreateProcessOutputLog(ProcessOutputLog processOutputLog)
        {
            await _context.ProcessOutputLogs.AddAsync(processOutputLog);
            await _context.SaveChangesAsync();
            return processOutputLog.Id;
        }

        public async Task ResolveProcessOutputLog(Guid processOutputLogId, Guid updatedUser)
        {
            var processOutputLog = await _context.ProcessOutputLogs.FirstOrDefaultAsync(p 
                                       => p.Id == processOutputLogId)
                                      ?? throw new NotFoundException("Process output log not found");
            if (processOutputLog.QualityAction != QualityAction.QUE)
                throw new InvalidQualityActionException("Operation valid only for QualityAction = 'QUE'");
            processOutputLog.QualityStatus = QualityStatus.RESOLVED;
            processOutputLog.CreatedUser = updatedUser;
            processOutputLog.CreatedTimestamp = DateTime.Now;
            await _context.SaveChangesAsync();
        }

        public async Task PendProcessOutputLog(Guid processOutputLogId, Guid updatedUser)
        {
            var processOutputLog = await _context.ProcessOutputLogs.FirstOrDefaultAsync(p 
                                       => p.Id == processOutputLogId)
                                      ?? throw new NotFoundException("Process output log not found");
            if (processOutputLog.QualityAction != QualityAction.QUE)
                throw new InvalidQualityActionException("Operation valid only for QualityAction = 'QUE'");
            processOutputLog.QualityStatus = QualityStatus.PENDING;
            processOutputLog.CreatedUser = updatedUser;
            processOutputLog.CreatedTimestamp = DateTime.Now;
            await _context.SaveChangesAsync();                     
        }
        public async Task<ProcessOutputLog> GetProcessOutputLog(Guid id)
        {
            return await _context.ProcessOutputLogs.FirstOrDefaultAsync(p => p.Id == id)
              ?? throw new NotFoundException("Process output log not found");
        }

        public  async Task<List<ProcessOutputLog>> GetProcessOutputLogsByBuildingId(Guid buildingId)
        {
           return await _context.ProcessOutputLogs.Include("Rule").Where(p => p.BldId == buildingId).ToListAsync();
        }

        public async Task<List<ProcessOutputLog>> GetPendingProcessOutputLogsByBuildingId(Guid buildingId, QualityStatus qualityStatus)
        {
            return await _context.ProcessOutputLogs.Include("Rule")
            .Where(p => p.BldId == buildingId && p.QualityStatus == qualityStatus).ToListAsync();
        }
        public async Task<List<ProcessOutputLog>> GetProcessOutputLogsByEntranceId(Guid entranceId)
        {
            return await _context.ProcessOutputLogs.Include("Rule").Where(p => p.EntId == entranceId).ToListAsync();
        }
        public async Task<List<ProcessOutputLog>> GetProcessOutputLogsByDwellingId(Guid dwellingId)
        {
            return await _context.ProcessOutputLogs.Include("Rule").Where(p => p.DwlId == dwellingId).ToListAsync();    
        }
    }
}
