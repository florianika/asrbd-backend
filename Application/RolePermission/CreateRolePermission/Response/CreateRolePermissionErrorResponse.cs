
namespace Application.RolePermission.CreateRolePermission.Response
{
    public class CreateRolePermissionErrorResponse : CreateRolePermissionResponse
    {
        public string? Message { get; internal set; }
        public string? Code { get; internal set; }
    }
}
