
using Application.Quality.AllBuildingsQualityCheck.Request;
using Application.Quality.AllBuildingsQualityCheck.Response;

namespace Application.Quality.AllBuildingsQualityCheck
{
    public interface IAllBuildingsQualityCheck : IQualityCheck<AllBuildingsQualityCheckRequest, AllBuildingsQualityCheckResponse>
    {
    }
}
