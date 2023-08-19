

using Application.DTO;
using Application.Enums;
using Application.Ports;
using Application.RolePermission.GetPermissionsByRole.Response;
using Application.RolePermission.GetPermissionsByRoleAndEntity.Request;
using Application.RolePermission.GetPermissionsByRoleAndEntity.Response;
using Domain.Enum;
using Microsoft.Extensions.Logging;

namespace Application.RolePermission.GetPermissionsByRoleAndEntity
{
    public class GetPermissionsByRoleAndEntity : IGetPermissionsByRoleAndEntity
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly ILogger _logger;
        public GetPermissionsByRoleAndEntity(IPermissionRepository permissionRepository, ILogger<GetPermissionsByRoleAndEntity> logger)
        {
            _permissionRepository = permissionRepository;
            _logger = logger;
        }
        public async Task<GetPermissionsByRoleAndEntityResponse> Execute(GetPermissionsByRoleAndEntityRequest request)
        {
            try
            {
                if (Enum.TryParse(request.Role, out AccountRole parsedRole))
                {
                    if (Enum.TryParse(request.EntityType, out EntityType parsedEntity))
                    {
                        var rolePermissions = await _permissionRepository.GetPermissionsByRoleAndEntity(parsedRole, parsedEntity);
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
                        return new GetPermissionsByRoleAndEntitySuccessResponse
                        {
                            RolePermissionsDTO = rolePermissionsDTO
                        };
                    }
                    else                         
                    {
                        return new GetPermissionsByRoleAndEntityErrorResponse
                        {
                            Message = Enum.GetName(ErrorCodes.EntityTypeIsNotCorrect),
                            Code = ErrorCodes.EntityTypeIsNotCorrect.ToString("D")
                        };
                    }
                }
                else
                {
                    return new GetPermissionsByRoleAndEntityErrorResponse
                    {
                        Message = Enum.GetName(ErrorCodes.AccountRoleIsNotCorrect),
                        Code = ErrorCodes.AccountRoleIsNotCorrect.ToString("D")
                    };
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                var response = new GetPermissionsByRoleAndEntityErrorResponse
                {
                    Message = Enum.GetName(ErrorCodes.AnUnexpectedErrorOcurred),
                    Code = ErrorCodes.AnUnexpectedErrorOcurred.ToString("D")
                };

                return response;
            }
        }
    }
}
