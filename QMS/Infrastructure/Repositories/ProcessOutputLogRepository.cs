using System;
using Application.Exceptions;
using Application.Ports;
using Domain;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

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

        public async Task<ProcessOutputLog> GetProcessOutputLog(Guid id)
        {
            return await _context.ProcessOutputLogs.FirstOrDefaultAsync(p => p.Id == id)
              ?? throw new NotFoundException("Process output log not found");
        }

        public async Task<List<ProcessOutputLog>> GetProcessOutputLogsByBuildingId(Guid buildingId)
        {
            return await _context.ProcessOutputLogs.Where(p => p.BldId == buildingId).ToListAsync();
        }
        public async Task<List<ProcessOutputLog>> GetProcessOutputLogsByEntranceId(Guid entranceId)
        {
            return await _context.ProcessOutputLogs.Where(p => p.EntId == entranceId).ToListAsync();
        }
    }
}
