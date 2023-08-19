
namespace Application.RolePermission.GetPermissionsByRole.Request
{
    public class GetPermissionsByRoleRequest : RolePermission.RequestRolePermission
    {
        public string Role { get; set; }
    }
}
