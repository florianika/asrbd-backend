

namespace Application.RolePermission.GetPermissionsByRole.Response
{
    public class GetPermissionsByRoleErrorResponse : GetPermissionsByRoleResponse
    {
        public string Message { get; set; }
        public string Code { get; set; }
    }
}
