using System;

namespace Application.Quality.BuildingQualityCheck.Response
{
    public class BuildingQualityCheckErrorResponse : BuildingQualityCheckResponse
    {
        public string? Message { get; set; }
        public string? Code { get; set; }
    }
}
