using Domain.Enum;

namespace Application.RolePermission.GetPermissionsByRoleAndEntity.Request
{
    public class GetPermissionsByRoleAndEntityRequest : RequestRolePermission
    {
        public AccountRole Role { get; set; }
        public EntityType EntityType { get; set; }
    }
}
