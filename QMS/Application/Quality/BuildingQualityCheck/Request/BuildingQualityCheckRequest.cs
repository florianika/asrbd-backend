using System;

namespace Application.Quality.BuildingQualityCheck.Request
{
    public class BuildingQualityCheckRequest : IRequest
    {
        public List<Guid>? BuildingIds { get; set; }
        public Guid ExecutionUser { get; set; }
    }
}
