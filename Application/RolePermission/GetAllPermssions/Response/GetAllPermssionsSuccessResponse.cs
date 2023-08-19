
using Application.DTO;

namespace Application.RolePermission.GetAllPermssions.Response
{
    public class GetAllPermssionsSuccessResponse : GetAllPermssionsResponse
    {
        public IEnumerable<RolePermissionDTO> RolePermissionsDTO { get; set; }
    }
}
