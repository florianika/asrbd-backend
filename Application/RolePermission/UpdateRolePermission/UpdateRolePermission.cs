

using Application.Enums;
using Application.Exceptions;
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
                var validationErrors = new List<string>();
                if (!Enum.IsDefined(typeof(AccountRole), request.Role))
                {
                    validationErrors.Add("Invalid role value.");
                }
                if (!Enum.IsDefined(typeof(EntityType), request.EntityType))
                {
                    validationErrors.Add("Invalid entity type value.");
                }

                if (!Enum.IsDefined(typeof(Permission), request.Permission))
                {
                    validationErrors.Add("Invalid permission value.");
                }

                if (validationErrors.Count > 0)
                {
                    throw new EnumExeption(validationErrors);
                }

                var rolePermission = await _permissionRepository.GetPermissionRoleById(request.Id);
                if (rolePermission == null)
                {
                    throw new NotFoundException("Permission not found");
                }

                var newRolePermission = new Domain.RolePermission.RolePermission
                {
                    Id = request.Id,
                    Role = request.Role,
                    EntityType = request.EntityType,
                    Permission = request.Permission,
                    VariableName = request.VariableName
                };

                await _permissionRepository.UpdateRolePermission(Id, newRolePermission);
                return new UpdateRolePermissionSuccessResponse
                {
                    Message = "Permission role updated"
                };


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
