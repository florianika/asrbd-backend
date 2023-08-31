using Application.Common.Translators;
using Application.Ports;
using Application.RolePermission.GetAllPermssions.Response;
using Application.User.GetAllUsers;
using Microsoft.Extensions.Logging;

namespace Application.RolePermission.GetAllPermssions
{
    public class GetAllPermissions : IGetAllPermissions
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly ILogger _logger;
        public GetAllPermissions(IPermissionRepository permissionRepository, ILogger<GetAllUsers> logger)
        {
            _permissionRepository = permissionRepository;
            _logger = logger;
        }
        public async Task<GetAllPermssionsResponse> Execute()
        {
            try
            {
                var rolePermissions = await _permissionRepository.GetAllPermissions();
                return new GetAllPermssionsSuccessResponse
                {
                    RolePermissionsDTO = Translator.ToDTOList(rolePermissions)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }
    }
}
