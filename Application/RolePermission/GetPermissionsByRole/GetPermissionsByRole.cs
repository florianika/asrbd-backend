using Application.Enums;
using Application.Exceptions;
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
                var validationErrors = new List<string>();
                if (!Enum.IsDefined(typeof(AccountRole), request.Role))
                {
                    validationErrors.Add("Invalid role value.");
                }
                if (validationErrors.Count > 0)
                {
                    throw new EnumExeption(validationErrors);
                }
                var rolePermissions = await _permissionRepository.GetPermissionsByRole(request.Role);
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
                return new GetPermissionsByRoleSuccessResponse
                {
                    RolePermissionsDTO = rolePermissionsDTO
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
