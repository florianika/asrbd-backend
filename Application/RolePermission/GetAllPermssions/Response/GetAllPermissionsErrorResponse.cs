using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.RolePermission.GetAllPermssions.Response
{
    public class GetAllPermissionsErrorResponse : GetAllPermssionsResponse
    {
        public string Message { get; set; }
        public string Code { get; set; }
    }
}
