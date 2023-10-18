using Domain.Enum;

namespace Application.RolePermission.ChangeRolePermission.Request
{
    public class ChangeRolePermissionRequest : RequestRolePermission
    {
        public long Id { get; set; }
        public Permission NewPermission { get; set; }
    }
}
