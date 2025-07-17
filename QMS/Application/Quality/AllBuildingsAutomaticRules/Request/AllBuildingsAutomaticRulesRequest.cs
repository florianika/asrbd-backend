namespace Application.Quality.AllBuildingsAutomaticRules.Request
{
    public class AllBuildingsAutomaticRulesRequest : IRequest
    {
        public Guid ExecutionUser { get; set; }
    }
}
