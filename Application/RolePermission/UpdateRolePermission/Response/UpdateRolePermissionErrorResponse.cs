namespace Application.RolePermission.UpdateRolePermission.Response
{
    #nullable disable
    public class UpdateRolePermissionErrorResponse: UpdateRolePermissionResponse
    {
        public string Message { get; internal set; }
        public string Code { get; internal set; }
    }
}
