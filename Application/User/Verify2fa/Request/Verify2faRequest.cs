namespace Application.User.Verify2fa.Request
{
    public class Verify2faRequest : User.Request
    {
        public required Guid UserId { get; set; }
        public required string Code { get; set; }
    }
}
