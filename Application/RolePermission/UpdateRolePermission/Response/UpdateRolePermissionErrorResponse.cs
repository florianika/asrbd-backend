using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.RolePermission.UpdateRolePermission.Response
{
    public class UpdateRolePermissionErrorResponse: UpdateRolePermissionResponse
    {
        public string Message { get; internal set; }
        public string Code { get; internal set; }
    }
}
