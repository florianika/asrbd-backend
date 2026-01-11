namespace Application.Building.TestBuildings.Request
{
    public class TestBuildingsRequest : Building.Request
    {
        public bool isAllBuildings { get; set; }
        public Guid CreatedUser { get; set; }
        public DateTime? StartAt { get; set; }
        public bool RunUpdates { get; set; }
    }
}
