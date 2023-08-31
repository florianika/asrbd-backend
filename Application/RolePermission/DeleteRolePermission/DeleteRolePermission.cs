using Application.Enums;
using Application.Exceptions;
using Application.Ports;
using Application.RolePermission.CreateRolePermission.Response;
using Application.RolePermission.DeleteRolePermission.Request;
using Application.RolePermission.DeleteRolePermission.Response;
using Application.User.GetUser;
using Microsoft.Extensions.Logging;

namespace Application.RolePermission.DeleteRolePermission
{
    public class DeleteRolePermission : IDeleteRolePermission
    {
        private readonly ILogger _logger;
        private readonly IPermissionRepository _permissionRepository;
        public DeleteRolePermission(ILogger<DeleteRolePermission> logger, IPermissionRepository permissionRepository)
        {
            _logger = logger;
            _permissionRepository = permissionRepository;
        }
        public async Task<DeleteRolePermissionResponse> Execute(DeleteRolePermissionRequest request)
        {
            await _permissionRepository.DeleteRolePermission(request.Id);
            return new DeleteRolePermissionSuccessResponse
            {
                Message = "Permission role deleted"
            };
        }
    }
}
