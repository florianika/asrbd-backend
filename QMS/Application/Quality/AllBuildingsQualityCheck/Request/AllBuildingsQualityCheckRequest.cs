namespace Application.Quality.AllBuildingsQualityCheck.Request
{
    public class AllBuildingsQualityCheckRequest : IRequest
    {
        public Guid ExecutionUser { get; set; }
    }
}
