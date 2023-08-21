
namespace Application.User.UpdateUserRole.Request
{
    public class UpdateUserRoleRequest : User.Request
    {
        public Guid UserId { get; set; }
        //FIXME this should be enum
        public string AccountRole { get; set; }
    }
}
