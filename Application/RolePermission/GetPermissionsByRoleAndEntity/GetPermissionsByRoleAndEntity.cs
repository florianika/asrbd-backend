using Application.Common.Translators;
using Application.Ports;
using Application.RolePermission.GetPermissionsByRoleAndEntity.Request;
using Application.RolePermission.GetPermissionsByRoleAndEntity.Response;
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
            var rolePermissions = await _permissionRepository.GetPermissionsByRoleAndEntity(request.Role, request.EntityType);
                
            return new GetPermissionsByRoleAndEntitySuccessResponse
            {
                RolePermissionsDto = Translator.ToDTOList(rolePermissions)
            };
        }
    }
}
