namespace Application.User
{
    public abstract class Request
    {
        public Guid RequestUserId { get; set; } = Guid.Empty;
        public string? RequestUserRole { get; set; } = null;
    }
}
