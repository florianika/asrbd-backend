namespace Application.RolePermission.GetPermissionsByRoleAndEntityAndVariable.Response
{
    public class GetPermissionsByRoleAndEntityAndVariableSuccessResponse : GetPermissionsByRoleAndEntityAndVariableResponse
    {
        public IEnumerable<RolePermissionDTO> RolePermissionsDto { get; set; } = null!;
    }
}
