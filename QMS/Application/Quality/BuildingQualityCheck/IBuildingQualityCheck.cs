using System;
using Application.Quality.BuildingQualityCheck.Request;
using Application.Quality.BuildingQualityCheck.Response;

namespace Application.Quality.BuildingQualityCheck
{
    public interface IBuildingQualityCheck : IQualityCheck<BuildingQualityCheckRequest, BuildingQualityCheckResponse>
    {
        
    }
}
