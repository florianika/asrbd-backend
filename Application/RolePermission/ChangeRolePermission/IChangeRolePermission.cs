using System;
using Application.RolePermission.ChangeRolePermission.Request;
using Application.RolePermission.ChangeRolePermission.Response;

namespace Application.RolePermission.ChangeRolePermission
{
    public interface IChangeRolePermission : IRolePermission<ChangeRolePermissionRequest, ChangeRolePermissionResponse>
    {
        
    }
}
