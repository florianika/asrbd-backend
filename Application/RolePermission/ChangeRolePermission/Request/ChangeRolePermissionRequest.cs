using System;
using Domain.Enum;

namespace Application.RolePermission.ChangeRolePermission.Request
{
    public class ChangeRolePermissionRequest : RolePermission.RequestRolePermission
    {
        public long Id { get; set; }
        public Permission NewPermission { get; set; }
    }
}
