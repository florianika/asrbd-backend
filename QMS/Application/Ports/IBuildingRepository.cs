namespace Application.Ports
{
    public interface IBuildingRepository
    {
        Task<int> CreateJob(Domain.Jobs job);
        Task<Domain.Jobs> GetJobById(int id);
        Task UpdateJob(Domain.Jobs job);
        Task ExecuteTestBuildingSP(int jobId, bool isAllBuildings);
    }
}
