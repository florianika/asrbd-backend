
namespace Application.RolePermission.GetPermissionsByRoleAndEntity.Request
{
    public class GetPermissionsByRoleAndEntityRequest : RolePermission.RequestRolePermission
    {
        //FIXME this should be enum
        public string Role { get; set; }
        //FIXME this should be enum
        public string EntityType { get; set; }
    }
}
