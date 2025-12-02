
namespace Application.Ports
{
    public interface IAnnualSnapshotExecutor
    {
        Task ExportAnnualSnapshotAsync(int downloadJobId);
    }
}
