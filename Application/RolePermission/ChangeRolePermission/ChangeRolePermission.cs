using System;
using Application.RolePermission.ChangeRolePermission.Request;
using Application.RolePermission.ChangeRolePermission.Response;
using Application.User.GetUser;
using Microsoft.Extensions.Logging;
using Application.Ports;

namespace Application.RolePermission.ChangeRolePermission
{
    public class ChangeRolePermission : IChangeRolePermission
    {
        
        private readonly ILogger _logger;
        private readonly IPermissionRepository _permissionRepository;

        public ChangeRolePermission(ILogger<GetUser> logger,IPermissionRepository permissionRepository) {
            _logger = logger;
            _permissionRepository = permissionRepository;
        }

        public async Task<ChangeRolePermissionResponse> Execute(ChangeRolePermissionRequest request) 
        {
            try
            {
                await _permissionRepository.ChangeRolePermission(request.Id, request.NewPermission);

                return new ChangeRolePermissionSuccessResponse
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
