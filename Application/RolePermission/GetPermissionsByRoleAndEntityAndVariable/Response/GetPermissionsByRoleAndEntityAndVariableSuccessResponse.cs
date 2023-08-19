using Application.DTO;

namespace Application.RolePermission.GetPermissionsByRoleAndEntityAndVariable.Response
{
    public class GetPermissionsByRoleAndEntityAndVariableSuccessResponse : GetPermissionsByRoleAndEntityAndVariableResponse
    {
        public IEnumerable<RolePermissionDTO> RolePermissionsDTO { get; set; }
    }
}
