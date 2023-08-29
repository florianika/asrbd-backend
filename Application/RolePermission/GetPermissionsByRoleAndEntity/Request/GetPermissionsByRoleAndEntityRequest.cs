
using Domain.Enum;

namespace Application.RolePermission.GetPermissionsByRoleAndEntity.Request
{
    public class GetPermissionsByRoleAndEntityRequest : RolePermission.RequestRolePermission
    {
        //FIXME this should be enum
        public AccountRole Role { get; set; }
        //FIXME this should be enum
        public EntityType EntityType { get; set; }
    }
}
