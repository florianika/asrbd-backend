using Domain.Enum;

namespace Application.RolePermission.GetPermissionsByRole.Request
{
    public class GetPermissionsByRoleRequest : RequestRolePermission
    {
        public AccountRole Role { get; set; }
    }
}
