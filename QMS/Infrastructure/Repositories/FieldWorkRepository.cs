
using Application.Exceptions;
using Application.Ports;
using Domain;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace Infrastructure.Repositories
{
    public class FieldWorkRepository : IFieldWorkRepository
    {
        private readonly DataContext _context;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public FieldWorkRepository(DataContext dataContext, IServiceScopeFactory serviceScopeFactory)
        {
            _context = dataContext;
            _serviceScopeFactory = serviceScopeFactory;
        }
        public async Task<List<FieldWork>> GetAllFieldWork()
        {
            return await _context.FieldWork.ToListAsync();
        }
        public async Task<int> CreateFieldWork(Domain.FieldWork fieldwork)
        {
            await _context.FieldWork.AddAsync(fieldwork);
            await _context.SaveChangesAsync();
            return fieldwork.FieldWorkId;
        }
        public async Task<Domain.FieldWork> GetFieldWork(int id)
        {
            return await _context.FieldWork.FirstOrDefaultAsync(x => x.FieldWorkId.Equals(id))
                ?? throw new NotFoundException("FieldWork not found");
        }

        public async Task UpdateFieldWork(FieldWork fieldwork)
        {
            _context.FieldWork.Update(fieldwork);
            await _context.SaveChangesAsync();
        }

        public async Task<FieldWork> GetActiveFieldWork()
        {
            return await _context.FieldWork.FirstOrDefaultAsync(x => x.fieldWorkStatus != Domain.Enum.FieldWorkStatus.CLOSED);
        }
    }
}
