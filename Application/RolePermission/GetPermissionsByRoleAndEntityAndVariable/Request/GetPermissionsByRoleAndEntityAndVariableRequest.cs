
namespace Application.RolePermission.GetPermissionsByRoleAndEntityAndVariable.Request
{
    public class GetPermissionsByRoleAndEntityAndVariableRequest : RolePermission.RequestRolePermission
    {
        public string Role { get; set; }
        public string EntityType { get; set; }
        public string VariableName { get; set; }
    }
}
