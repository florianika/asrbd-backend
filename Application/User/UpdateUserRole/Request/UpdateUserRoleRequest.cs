
namespace Application.User.UpdateUserRole.Request
{
    public class UpdateUserRoleRequest : User.Request
    {
        public Guid UserId { get; set; }
        public string AccountRole { get; set; }
    }
}
