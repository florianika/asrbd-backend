
using Application.Ports;
using Domain;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Repositories
{
    public class FieldWorkRepository : IFiledWorkRepository
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
    }
}
