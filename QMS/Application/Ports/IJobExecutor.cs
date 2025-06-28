namespace Application.Ports
{
    public interface IJobExecutor
    {
        Task ExecuteStatisticsJobAsync(int jobId);
    }
}
