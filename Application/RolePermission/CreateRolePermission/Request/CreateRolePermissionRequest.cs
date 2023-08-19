using Domain.Enum;

namespace Application.RolePermission.Request
{
    public class CreateRolePermissionRequest : RolePermission.RequestRolePermission
    {
        public string Role { get; set; }
        public string EntityType { get; set; }
        public string VariableName { get; set; }
        public string Permission{ get; set; }

    }
}
