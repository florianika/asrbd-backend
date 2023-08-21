
namespace Application.RolePermission.GetPermissionsByRole.Request
{
    public class GetPermissionsByRoleRequest : RolePermission.RequestRolePermission
    {
        //FIXME this should be enum
        public string Role { get; set; }
    }
}
