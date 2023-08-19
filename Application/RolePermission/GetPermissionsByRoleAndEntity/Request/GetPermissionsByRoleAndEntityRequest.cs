
namespace Application.RolePermission.GetPermissionsByRoleAndEntity.Request
{
    public class GetPermissionsByRoleAndEntityRequest : RolePermission.RequestRolePermission
    {
        public string Role { get; set; }
        public string EntityType { get; set; }
    }
}
