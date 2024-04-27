

namespace Application.Quality.AutomaticRules.Request
{
    public class AutomaticRulesRequest : IRequest
    {
        public List<Guid> BuildingIds { get; set; } = null!;
        public Guid ExecutionUser { get; set; }
    }
}
