
using Domain.Enum;

namespace Application.RolePermission.GetPermissionsByRoleAndEntityAndVariable.Request
{
    public class GetPermissionsByRoleAndEntityAndVariableRequest : RequestRolePermission
    {
        public AccountRole Role { get; set; }
        public EntityType EntityType { get; set; }
        public string VariableName { get; set; } = null!;
    }
}
