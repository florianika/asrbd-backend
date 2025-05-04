
using Application.Ports;
using Domain;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
    }
}
