

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
        public async Task<UpdateRolePermissionResponse> Execute(UpdateRolePermissionRequest request)
        {

            await _permissionRepository.UpdateRolePermission(request);
            return new UpdateRolePermissionSuccessResponse
            {
                Message = "Permission role updated"
            };
        }
    }
}
