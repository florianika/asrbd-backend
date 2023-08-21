
namespace Application.RolePermission.UpdateRolePermission.Request
{
    public class UpdateRolePermissionRequest : RolePermission.RequestRolePermission
    {
        public long Id { get; set; }
        //FIXME this should be entity
        public string Role { get; set; }
        //FIXME this should be entity
        public string EntityType { get; set; }
        public string VariableName { get; set; }
        //FIXME this should be entity
        public string Permission { get; set; }
    }
}
