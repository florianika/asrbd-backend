using Domain;

namespace Application.Ports
{
    public interface IDownloadJobRepository
    {
        Task<DownloadJob> CreateDownloadJob(DownloadJob job);
        Task<DownloadJob?> GetDownloadJobById(int id);
        Task UpdateDownloadJob(DownloadJob job);
        Task<DownloadJob?> GetDownloadJobByYear(int referenceYear);
        Task<IReadOnlyList<DownloadJob>> GetAllDownloadJobs();
    }
}
