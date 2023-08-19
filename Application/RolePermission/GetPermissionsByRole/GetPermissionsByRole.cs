
using Application.DTO;
using Application.Enums;
using Application.Ports;
using Application.RolePermission.GetAllPermssions.Response;
using Application.RolePermission.GetPermissionsByRole.Request;
using Application.RolePermission.GetPermissionsByRole.Response;
using Application.User.UpdateUserRole.Response;
using Domain.Enum;
using Microsoft.Extensions.Logging;

namespace Application.RolePermission.GetPermissionsByRole
{
    public class GetPermissionsByRole : IGetPermissionsByRole
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly ILogger _logger;
        public GetPermissionsByRole(IPermissionRepository permissionRepository, ILogger<GetPermissionsByRole> logger)
        {
            _permissionRepository = permissionRepository;
            _logger = logger;
        }
        public async Task<GetPermissionsByRoleResponse> Execute(GetPermissionsByRoleRequest request)
        {
            try
            {
                if (Enum.TryParse(request.Role, out AccountRole parsedRole))
                {
                    var rolePermissions = await _permissionRepository.GetPermissionsByRole(parsedRole);
                    var rolePermissionsDTO = new List<RolePermissionDTO>();
                    foreach (var rolePermission in rolePermissions)
                    {
                        var rolePermissionDTO = new RolePermissionDTO();
                        rolePermissionDTO.Id = rolePermission.Id;
                        rolePermissionDTO.VariableName = rolePermission.VariableName;
                        rolePermissionDTO.Role = rolePermission.Role.ToString();
                        rolePermissionDTO.EntityType = rolePermission.EntityType.ToString();
                        rolePermissionDTO.Permission = rolePermission.Permission.ToString();
                        rolePermissionsDTO.Add(rolePermissionDTO);
                    }
                    return new GetPermissionsByRoleSuccessResponse
                    {
                        RolePermissionsDTO = rolePermissionsDTO
                    };
                }
                else
                {
                    return new GetPermissionsByRoleErrorResponse
                    {
                        Message = Enum.GetName(ErrorCodes.AccountRoleIsNotCorrect),
                        Code = ErrorCodes.AccountRoleIsNotCorrect.ToString("D")
                    };
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                var response = new GetPermissionsByRoleErrorResponse
                {
                    Message = Enum.GetName(ErrorCodes.AnUnexpectedErrorOcurred),
                    Code = ErrorCodes.AnUnexpectedErrorOcurred.ToString("D")
                };

                return response;
            }
        }
    }
}
