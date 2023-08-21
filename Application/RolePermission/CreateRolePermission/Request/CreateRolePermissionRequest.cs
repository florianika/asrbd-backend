using Domain.Enum;

namespace Application.RolePermission.Request
{
    public class CreateRolePermissionRequest : RolePermission.RequestRolePermission
    {
        //FIXME this should be enum
        public string Role { get; set; }
        //FIXME this should be enum
        public string EntityType { get; set; }
        public string VariableName { get; set; }
        //FIXME this should be enum
        public string Permission{ get; set; }

    }
}
