
namespace Application.RolePermission.UpdateRolePermission.Request
{
    public class UpdateRolePermissionRequest : RolePermission.RequestRolePermission
    {
        public long Id { get; set; }
        public string Role { get; set; }
        public string EntityType { get; set; }
        public string VariableName { get; set; }
        public string Permission { get; set; }
    }
}
