
namespace Application.RolePermission.GetPermissionsByRoleAndEntity.Request
{
    public class GetPermissionsByRoleAndEntityRequest : RolePermission.RequestRolePermission
    {
        //FIXME this should be entity
        public string Role { get; set; }
        //FIXME this should be entity
        public string EntityType { get; set; }
    }
}
