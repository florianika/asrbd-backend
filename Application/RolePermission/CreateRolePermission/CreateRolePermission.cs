

using Application.Enums;
using Application.Ports;
using Application.RolePermission.CreateRolePermission.Response;
using Application.RolePermission.Request;
using Application.User.CreateUser.Response;
using Application.User.GetUser;
using Application.User.UpdateUserRole.Response;
using Domain.Enum;
using Microsoft.Extensions.Logging;

namespace Application.RolePermission.CreateRolePermission
{
    public class CreateRolePermission : ICreateRolePermission
    {
        private readonly ILogger _logger;
        private readonly IPermissionRepository _permissionRepository;
        public CreateRolePermission(ILogger<GetUser> logger,IPermissionRepository permissionRepository)
        {
            _logger = logger;
            _permissionRepository = permissionRepository;
        }
        public async Task<CreateRolePermissionResponse> Execute(CreateRolePermissionRequest request)
        {
            try
            {
                var rolePermission = new Domain.RolePermission.RolePermission();

                if (Enum.TryParse(request.Role, out AccountRole parsedRole))
                {
                    rolePermission.Role = parsedRole;
                }
                else
                {
                    return new CreateRolePermissionErrorResponse
                    {
                        Message = Enum.GetName(ErrorCodes.AccountRoleIsNotCorrect),
                        Code = ErrorCodes.AccountRoleIsNotCorrect.ToString("D")
                    };
                }

                if (Enum.TryParse(request.EntityType, out EntityType parsedEntity))
                {
                    rolePermission.EntityType = parsedEntity;
                }
                else
                {
                    return new CreateRolePermissionErrorResponse
                    {
                        Message = Enum.GetName(ErrorCodes.EntityTypeIsNotCorrect),
                        Code = ErrorCodes.EntityTypeIsNotCorrect.ToString("D")
                    };
                }

                if (Enum.TryParse(request.Permission, out Permission parsedPermission))
                {
                    rolePermission.Permission = parsedPermission;
                }
                else
                {
                    return new CreateRolePermissionErrorResponse
                    {
                        Message = Enum.GetName(ErrorCodes.PermissionIsNotCorrect),
                        Code = ErrorCodes.PermissionIsNotCorrect.ToString("D")
                    };
                }
                rolePermission.VariableName = request.VariableName;

                var result = await _permissionRepository.CreateRolePermission(rolePermission);
                if (result == null)
                {
                    return new CreateRolePermissionErrorResponse
                    {
                        Message = Enum.GetName(ErrorCodes.AnUnexpectedErrorOcurred),
                        Code = ErrorCodes.AnUnexpectedErrorOcurred.ToString("D")
                    };
                }
                else
                {
                    return new CreateRolePermissionSuccessResponse
                    {
                        Id = result
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return new CreateRolePermissionErrorResponse
                {
                    Message = Enum.GetName(ErrorCodes.AnUnexpectedErrorOcurred),
                    Code = ErrorCodes.AnUnexpectedErrorOcurred.ToString("D")
                };
            }
        }


    }
}
