using Application.DTO;
using Application.Enums;
using Application.Ports;
using Application.RolePermission.GetAllPermssions.Response;
using Application.User.GetAllUsers;
using Application.User.GetAllUsers.Response;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                var rolePermissionsDTO = new List<RolePermissionDTO>();
                foreach (var rolePermission in rolePermissions)
                {
                    var rolePermissionDTO = new RolePermissionDTO();
                    rolePermissionDTO.Id = rolePermission.Id;
                    rolePermissionDTO.VariableName = rolePermission.VariableName;
                    rolePermissionDTO.Role = rolePermission.Role.ToString();
                    rolePermissionDTO.EntityType = rolePermission.EntityType.ToString(); 
                    rolePermissionDTO.Permission = rolePermission.Permission.ToString();
                    rolePermissionsDTO.Add(rolePermissionDTO);
                }
                return new GetAllPermssionsSuccessResponse
                {
                    RolePermissionsDTO = rolePermissionsDTO
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                var response = new GetAllPermissionsErrorResponse
                {
                    Message = Enum.GetName(ErrorCodes.AnUnexpectedErrorOcurred),
                    Code = ErrorCodes.AnUnexpectedErrorOcurred.ToString("D")
                };

                return response;
            }
        }
    }
}
