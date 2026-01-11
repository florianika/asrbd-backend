namespace Application.Building.TestBuildings.Response
{
    public class TestBuildingsSuccessResponse : TestBuildingsResponse
    {
        public int JobId { get; set; }
        public string? HangfireJobId { get; set; }
    }
}
