using Application.Ports;
using Domain;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class DownloadJobRepository : IDownloadJobRepository
    {
        private readonly DataContext _context;

        public DownloadJobRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<DownloadJob> CreateDownloadJob(DownloadJob job)
        {
            await _context.DownloadJobs.AddAsync(job);
            await _context.SaveChangesAsync();
            return job;
        }

        public async Task<IReadOnlyList<DownloadJob>> GetAllDownloadJobs()
        {
            return await _context.DownloadJobs
           .OrderByDescending(x => x.ReferenceYear)
           .ThenByDescending(x => x.CreatedAt)
           .ToListAsync();
        }

        public async Task<DownloadJob?> GetDownloadJobById(int id)
        {
            return await _context.DownloadJobs
               .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<DownloadJob?> GetDownloadJobByYear(int referenceYear)
        {
            return await _context.DownloadJobs
            .FirstOrDefaultAsync(x => x.ReferenceYear == referenceYear);
        }

        public async Task UpdateDownloadJob(DownloadJob job)
        {
            _context.DownloadJobs.Update(job);
            await _context.SaveChangesAsync();
        }
    }
}
