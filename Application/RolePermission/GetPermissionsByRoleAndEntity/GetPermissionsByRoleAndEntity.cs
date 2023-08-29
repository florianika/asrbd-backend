using Application.Enums;
using Application.Exceptions;
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
                var validationErrors = new List<string>();
                if (!Enum.IsDefined(typeof(AccountRole), request.Role))
                {
                    validationErrors.Add("Invalid role value.");
                }
                if (!Enum.IsDefined(typeof(EntityType), request.EntityType))
                {
                    validationErrors.Add("Invalid entity type value.");
                }

                if (validationErrors.Count > 0)
                {
                    throw new EnumExeption(validationErrors);
                }
                var rolePermissions = await _permissionRepository.GetPermissionsByRoleAndEntity(request.Role, request.EntityType);
                var rolePermissionsDTO = new List<RolePermissionDTO>();
                foreach (var rolePermission in rolePermissions)
                {
                    var rolePermissionDTO = new RolePermissionDTO();
                    rolePermissionDTO.Id = rolePermission.Id;
                    rolePermissionDTO.VariableName = rolePermission.VariableName;
                    rolePermissionDTO.Role = rolePermission.Role;
                    rolePermissionDTO.EntityType = rolePermission.EntityType;
                    rolePermissionDTO.Permission = rolePermission.Permission;
                    rolePermissionsDTO.Add(rolePermissionDTO);
                }
                return new GetPermissionsByRoleAndEntitySuccessResponse
                {
                    RolePermissionsDTO = rolePermissionsDTO
                };

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
