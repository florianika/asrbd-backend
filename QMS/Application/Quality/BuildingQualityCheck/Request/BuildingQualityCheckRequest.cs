using System;

namespace Application.Quality.BuildingQualityCheck.Request
{
    public class BuildingQualityCheckRequest : IRequest
    {
        public Guid BuildingId { get; set; }
        public Guid ExecutionUser { get; set; }
    }
}
