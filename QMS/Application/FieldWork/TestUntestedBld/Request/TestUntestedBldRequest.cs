namespace Application.FieldWork.TestUntestedBld.Request
{
    public class TestUntestedBldRequest : FieldWork.Request
    {
        public int Id { get; set; } //fieldworkId
        public Guid CreatedUser { get; set; }
    }
}
