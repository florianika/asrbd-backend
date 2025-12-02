using Application.Building.CreateAnnualSnapshot.Request;
using Application.Building.CreateAnnualSnapshot.Response;

namespace Application.Building.CreateAnnualSnapshot
{
    public interface ICreateAnnualSnapshot : IBuilding<CreateAnnualSnapshotRequest, CreateAnnualSnapshotResponse>
    {
    }
}
