using System;
using Application.Quality.BuildingQualityCheck.Request;
using Application.Quality.BuildingQualityCheck.Response;
using Microsoft.Extensions.Logging;

namespace Application.Quality.BuildingQualityCheck
{
    public class BuidlingQualityCheck : IBuildingQualityCheck
    {
        private readonly ILogger _logger;

        public BuidlingQualityCheck(ILogger logger) {
            _logger = logger;
        }
        public Task<BuildingQualityCheckResponse> Execute(BuildingQualityCheckRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
