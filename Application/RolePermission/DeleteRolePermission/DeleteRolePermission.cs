using Application.Enums;
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
            try
            {
                var rolePermission = await _permissionRepository.GetPermissionRoleById(request.Id);
                if(rolePermission == null)
                {
                    return new DeleteRolePermissionErrorResponse
                    {
                        Message = Enum.GetName(ErrorCodes.PermissionRoleNotExist),
                        Code = ErrorCodes.PermissionRoleNotExist.ToString("D")
                    };
                }
                await _permissionRepository.DeleteRolePermission(rolePermission);
                return new DeleteRolePermissionSuccessResponse
                {
                    Message = "Permission role deleted"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return new DeleteRolePermissionErrorResponse
                {
                    Message = Enum.GetName(ErrorCodes.AnUnexpectedErrorOcurred),
                    Code = ErrorCodes.AnUnexpectedErrorOcurred.ToString("D")
                };
            }
        }
    }
}
