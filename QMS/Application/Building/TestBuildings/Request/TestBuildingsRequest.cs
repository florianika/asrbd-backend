namespace Application.Building.TestBuildings.Request
{
    public class TestBuildingsRequest : Building.Request
    {
        public bool isAllBuildings { get; set; }
        public Guid CreatedUser { get; set; }
    }
}
