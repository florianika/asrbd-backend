

using Application.Enums;
using Application.Ports;
using Application.RolePermission.DeleteRolePermission.Response;
using Application.RolePermission.GetPermissionsByRoleAndEntityAndVariable.Response;
using Application.RolePermission.UpdateRolePermission.Request;
using Application.RolePermission.UpdateRolePermission.Response;
using Domain.Enum;
using Microsoft.Extensions.Logging;

namespace Application.RolePermission.UpdateRolePermission
{
    public class UpdateRolePermission : IUpdateRolePermission
    {
        private readonly ILogger _logger;
        private readonly IPermissionRepository _permissionRepository;
        public UpdateRolePermission(ILogger<UpdateRolePermission> logger, IPermissionRepository permissionRepository)
        {
            _logger = logger;
            _permissionRepository = permissionRepository;
        }
        public async Task<UpdateRolePermissionResponse> Execute(long Id, UpdateRolePermissionRequest request)
        {
            try
            {
                if (Id != request.Id)
                {
                    return new UpdateRolePermissionErrorResponse
                    {
                        Message = Enum.GetName(ErrorCodes.BadRequest),
                        Code = ErrorCodes.BadRequest.ToString("D")
                    };
                }
                var rolePermission = await _permissionRepository.GetPermissionRoleById(request.Id);
                if (rolePermission == null)
                {
                    return new UpdateRolePermissionErrorResponse
                    {
                        Message = Enum.GetName(ErrorCodes.PermissionRoleNotExist),
                        Code = ErrorCodes.PermissionRoleNotExist.ToString("D")
                    };
                }
                if (Enum.TryParse(request.Role, out AccountRole parsedRole))
                {
                    if (Enum.TryParse(request.EntityType, out EntityType parsedEntity))
                    {
                        if (Enum.TryParse(request.Permission, out Permission parsedPermission))
                        {
                            var newRolePermission = new Domain.RolePermission.RolePermission
                            {
                                Id = request.Id,
                                Role = parsedRole,
                                EntityType = parsedEntity,
                                Permission = parsedPermission,
                                VariableName = request.VariableName
                            };
                            
                            await _permissionRepository.UpdateRolePermission(Id, newRolePermission);
                            return new UpdateRolePermissionSuccessResponse
                            {
                                Message = "Permission role updated"
                            };

                        }
                        else
                        {
                            return new UpdateRolePermissionErrorResponse
                            {
                                Message = Enum.GetName(ErrorCodes.PermissionIsNotCorrect),
                                Code = ErrorCodes.PermissionIsNotCorrect.ToString("D")
                            };
                        }

                    }
                    else
                    {
                        return new UpdateRolePermissionErrorResponse
                        {
                            Message = Enum.GetName(ErrorCodes.EntityTypeIsNotCorrect),
                            Code = ErrorCodes.EntityTypeIsNotCorrect.ToString("D")
                        };
                    }
                }
                else
                {
                    return new UpdateRolePermissionErrorResponse
                    {
                        Message = Enum.GetName(ErrorCodes.AccountRoleIsNotCorrect),
                        Code = ErrorCodes.AccountRoleIsNotCorrect.ToString("D")
                    };
                }


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return new UpdateRolePermissionErrorResponse
                {
                    Message = Enum.GetName(ErrorCodes.AnUnexpectedErrorOcurred),
                    Code = ErrorCodes.AnUnexpectedErrorOcurred.ToString("D")
                };
            }
        }
    }
}
