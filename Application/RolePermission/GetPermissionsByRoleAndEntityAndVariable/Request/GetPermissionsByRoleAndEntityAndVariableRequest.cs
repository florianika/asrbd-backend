
namespace Application.RolePermission.GetPermissionsByRoleAndEntityAndVariable.Request
{
    public class GetPermissionsByRoleAndEntityAndVariableRequest : RolePermission.RequestRolePermission
    {
        //FIXME this should be enum
        public string Role { get; set; }
        //FIXME this should be enum
        public string EntityType { get; set; }
        public string VariableName { get; set; }
    }
}
