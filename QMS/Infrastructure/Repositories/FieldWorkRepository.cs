
using Application.Exceptions;
using Application.Ports;
using Domain;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using Domain.Enum;

namespace Infrastructure.Repositories
{
    public class FieldWorkRepository : IFieldWorkRepository
    {
        private readonly DataContext _context;
        public FieldWorkRepository(DataContext dataContext)
        {
            _context = dataContext;
        }
        public async Task<List<FieldWork>> GetAllFieldWork()
        {
            return await _context.FieldWorks.ToListAsync();
        }
        public async Task<int> CreateFieldWork(Domain.FieldWork fieldwork)
        {
            await _context.FieldWorks.AddAsync(fieldwork);
            await _context.SaveChangesAsync();
            return fieldwork.FieldWorkId;
        }
        public async Task<FieldWork> GetFieldWork(int id)
        {
            return await _context.FieldWorks.FirstOrDefaultAsync(x => x.FieldWorkId.Equals(id))
                ?? throw new NotFoundException("FieldWork not found");
        }
        
        public async Task<FieldWork> GetFieldWorkByIdAndStatus(int id, FieldWorkStatus status)
        {
            return await _context.FieldWorks.FirstOrDefaultAsync(x => x.FieldWorkId.Equals(id) 
                                                                      && x.FieldWorkStatus == status)
                   ?? throw new NotFoundException("FieldWork with status NEW not found");
        }
        public async Task UpdateFieldWork(FieldWork fieldwork)
        {
            _context.FieldWorks.Update(fieldwork);
            await _context.SaveChangesAsync();
        }

        public async Task<FieldWork> GetActiveFieldWork()
        {
            return await _context.FieldWorks.FirstOrDefaultAsync(x => x.FieldWorkStatus != Domain.Enum.FieldWorkStatus.CLOSED)
                ?? throw new NotFoundException("FieldWork not found");
        }
    }
}
