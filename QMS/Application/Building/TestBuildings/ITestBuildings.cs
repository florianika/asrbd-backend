using Application.Building.TestBuildings.Request;
using Application.Building.TestBuildings.Response;

namespace Application.Building.TestBuildings
{
    public interface ITestBuildings : IBuilding<TestBuildingsRequest, TestBuildingsResponse>
    {
    }
}
