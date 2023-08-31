using Application.Exceptions;
using Application.Ports;
using Application.RolePermission.CreateRolePermission.Response;
using Application.RolePermission.Request;
using Application.User.GetUser;
using Domain.Enum;
using Microsoft.Extensions.Logging;

namespace Application.RolePermission.CreateRolePermission
{
    public class CreateRolePermission : ICreateRolePermission
    {
        private readonly ILogger _logger;
        private readonly IPermissionRepository _permissionRepository;
        public CreateRolePermission(ILogger<GetUser> logger,IPermissionRepository permissionRepository)
        {
            _logger = logger;
            _permissionRepository = permissionRepository;
        }
        public async Task<CreateRolePermissionResponse> Execute(CreateRolePermissionRequest request)
        {
            try
            {
                var rolePermission = new Domain.RolePermission
                {
                    Role = request.Role,
                    EntityType = request.EntityType,
                    VariableName = request.VariableName,
                    Permission = request.Permission
                };
                var result = await _permissionRepository.CreateRolePermission(rolePermission);
                return new CreateRolePermissionSuccessResponse
                {
                    Id = result
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
