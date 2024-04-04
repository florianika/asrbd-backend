using Application.Enums;
using Application.Exceptions;
using Application.Ports;
using Application.RolePermission.GetPermissionsByRoleAndEntity.Response;
using Application.RolePermission.GetPermissionsByRoleAndEntityAndVariable.Request;
using Application.RolePermission.GetPermissionsByRoleAndEntityAndVariable.Response;
using Domain.Enum;
using Microsoft.Extensions.Logging;

namespace Application.RolePermission.GetPermissionsByRoleAndEntityAndVariable
{
    public class GetPermissionsByRoleAndEntityAndVariable : IGetPermissionsByRoleAndEntityAndVariable
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly ILogger _logger;
        public GetPermissionsByRoleAndEntityAndVariable(IPermissionRepository permissionRepository, ILogger<GetPermissionsByRoleAndEntityAndVariable> logger)
        {
            _permissionRepository = permissionRepository;
            _logger = logger;
        }
        public async Task<GetPermissionsByRoleAndEntityAndVariableResponse> Execute(GetPermissionsByRoleAndEntityAndVariableRequest request)
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

                var rolePermissions = await _permissionRepository.GetPermissionsByRoleAndEntityAndVariable(request.Role, request.EntityType, request.VariableName);
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
                return new GetPermissionsByRoleAndEntityAndVariableSuccessResponse
                {
                    RolePermissionsDto = rolePermissionsDTO
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
