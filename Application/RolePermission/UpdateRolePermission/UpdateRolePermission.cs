using Application.Ports;
using Application.RolePermission.UpdateRolePermission.Request;
using Application.RolePermission.UpdateRolePermission.Response;
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

            var rolePermission = await _permissionRepository.GetPermissionRoleById(request.Id);
            rolePermission.EntityType = request.EntityType;
            rolePermission.Permission = request.Permission;
            rolePermission.Role = request.Role;
            rolePermission.VariableName = request.VariableName;

            await _permissionRepository.UpdateRolePermission(rolePermission);

            return new UpdateRolePermissionSuccessResponse
            {
                Message = "Permission role updated"
            };
        }
    }
}
