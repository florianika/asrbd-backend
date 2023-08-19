

using Application.DTO;

namespace Application.RolePermission.GetPermissionsByRole.Response
{
    public class GetPermissionsByRoleSuccessResponse : GetPermissionsByRoleResponse
    {
        public IEnumerable<RolePermissionDTO> RolePermissionsDTO { get; set; }
    }
}
