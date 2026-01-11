namespace Application.Ports
{
    public interface IBuildingRepository
    {
        Task<int> CreateJob(Domain.Jobs job);
        Task<Domain.Jobs> GetJobById(int id);
        Task UpdateJob(Domain.Jobs job);
        Task ExecuteTestBuildingSP(int jobId, bool isAllBuildings, bool runUpdates);
        Task<List<Domain.Jobs>> GetRunningJobs();
        Task<List<Domain.DownloadJob>> GetAllAnnualSnapshots();
        Task<Domain.DownloadJob> GetAnnualSnapshotById(int id);
    }
}
