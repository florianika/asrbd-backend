namespace Application.RolePermission.GetPermissionsByRoleAndEntity.Response
{
    public class GetPermissionsByRoleAndEntitySuccessResponse : GetPermissionsByRoleAndEntityResponse
    {
        public IEnumerable<RolePermissionDTO> RolePermissionsDto { get; set; } = null!;
    }
}
