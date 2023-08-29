
using Domain.Enum;

namespace Application.RolePermission.GetPermissionsByRoleAndEntityAndVariable.Request
{
    public class GetPermissionsByRoleAndEntityAndVariableRequest : RolePermission.RequestRolePermission
    {
        //FIXME this should be enum
        public AccountRole Role { get; set; }
        //FIXME this should be enum
        public EntityType EntityType { get; set; }
        public string VariableName { get; set; }
    }
}
