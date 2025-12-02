using Application.Building.GetAllAnnualSnapshots.Response;

namespace Application.Building.GetAllAnnualSnapshots
{
    public interface IGetAllAnnualSnapshots
    {
        public Task<GetAllAnnualSnapshotsResponse> Execute();
    }
}
