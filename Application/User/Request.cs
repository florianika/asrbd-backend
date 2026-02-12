using Domain.Enum;

namespace Application.User
{
    public abstract class Request
    {
        public Guid RequestUserId { get; set; } = Guid.Empty;
        public AccountRole RequestUserRole { get; set; } = AccountRole.USER;
    }
}
