

namespace Application.RolePermission.GetPermissionsByRoleAndEntity.Response
{
    public class GetPermissionsByRoleAndEntityErrorResponse : GetPermissionsByRoleAndEntityResponse
    {
        public string Message { get; set; } = null!;
        public string Code { get; set; } = null!;
    }
}
