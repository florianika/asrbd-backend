
namespace Application.RolePermission.DeleteRolePermission.Response
{
    public class DeleteRolePermissionErrorResponse : DeleteRolePermissionResponse
    {
        public string? Message { get; internal set; }
        public string? Code { get; internal set; }
    }
}
