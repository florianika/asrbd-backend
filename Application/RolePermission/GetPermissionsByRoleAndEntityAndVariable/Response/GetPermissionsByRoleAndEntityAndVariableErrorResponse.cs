
namespace Application.RolePermission.GetPermissionsByRoleAndEntityAndVariable.Response
{
    public class GetPermissionsByRoleAndEntityAndVariableErrorResponse : GetPermissionsByRoleAndEntityAndVariableResponse
    {
        public string Message { get; set; } = null!;
        public string Code { get; set; } = null!;
    }
}
