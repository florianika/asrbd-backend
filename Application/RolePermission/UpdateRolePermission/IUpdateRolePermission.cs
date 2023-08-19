using Application.RolePermission.CreateRolePermission.Response;
using Application.RolePermission.Request;
using Application.RolePermission.UpdateRolePermission.Request;
using Application.RolePermission.UpdateRolePermission.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.RolePermission.UpdateRolePermission
{
    public interface IUpdateRolePermission : IRolePermissionParam<UpdateRolePermissionRequest, UpdateRolePermissionResponse>
    {
    }
}
