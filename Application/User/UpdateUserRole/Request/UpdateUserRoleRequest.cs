
using Domain.Enum;

namespace Application.User.UpdateUserRole.Request
{
    public class UpdateUserRoleRequest : User.Request
    {
        public Guid UserId { get; set; }
        //FIXME this should be enum
        public AccountRole AccountRole { get; set; }
    }
}
