namespace Application.Quality.SetBldToUntested.Request
{
    public class SetBldToUntestedRequest : IRequest
    {
        public List<Guid> BuildingIds { get; set; } = null!;
    }
}
