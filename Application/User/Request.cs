namespace Application.User
{
    public abstract class Request
    {
        public Guid RequestUserId { get; set; }
        public string? RequestUserRole { get; set; }
    }
}
