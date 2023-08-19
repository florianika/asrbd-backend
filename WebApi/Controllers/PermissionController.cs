using Application.RolePermission.CreateRolePermission;
using Application.RolePermission.CreateRolePermission.Response;
using Application.RolePermission.DeleteRolePermission;
using Application.RolePermission.DeleteRolePermission.Request;
using Application.RolePermission.DeleteRolePermission.Response;
using Application.RolePermission.GetAllPermssions;
using Application.RolePermission.GetAllPermssions.Response;
using Application.RolePermission.GetPermissionsByRole;
using Application.RolePermission.GetPermissionsByRole.Request;
using Application.RolePermission.GetPermissionsByRole.Response;
using Application.RolePermission.GetPermissionsByRoleAndEntity;
using Application.RolePermission.GetPermissionsByRoleAndEntity.Request;
using Application.RolePermission.GetPermissionsByRoleAndEntity.Response;
using Application.RolePermission.GetPermissionsByRoleAndEntityAndVariable;
using Application.RolePermission.GetPermissionsByRoleAndEntityAndVariable.Request;
using Application.RolePermission.GetPermissionsByRoleAndEntityAndVariable.Response;
using Application.RolePermission.Request;
using Application.RolePermission.UpdateRolePermission;
using Application.RolePermission.UpdateRolePermission.Request;
using Application.RolePermission.UpdateRolePermission.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly CreateRolePermission _createRolePermissionService;
        private readonly GetAllPermissions _getAllPermissionsService;
        private readonly GetPermissionsByRole _getPermissionsByRoleService;
        private readonly GetPermissionsByRoleAndEntity _getPermissionsByRoleAndEntityService;
        private readonly GetPermissionsByRoleAndEntityAndVariable _getPermissionsByRoleAndEntityAndVariableService;
        private readonly DeleteRolePermission _deleteRolePermissionService;
        private readonly UpdateRolePermission _updateRolePermissionService;
        public PermissionController(CreateRolePermission createRolePermissionService,
            GetAllPermissions getAllPermissionsService,
            GetPermissionsByRole getPermissionsByRoleService,
            GetPermissionsByRoleAndEntity getPermissionsByRoleAndEntityService,
            GetPermissionsByRoleAndEntityAndVariable getPermissionsByRoleAndEntityAndVariable,
            DeleteRolePermission deleteRolePermissionService,
            UpdateRolePermission updateRolePermissionService)
        {
            _createRolePermissionService = createRolePermissionService;
            _getAllPermissionsService = getAllPermissionsService;
            _getPermissionsByRoleService = getPermissionsByRoleService;
            _getPermissionsByRoleAndEntityService = getPermissionsByRoleAndEntityService;
            _getPermissionsByRoleAndEntityAndVariableService = getPermissionsByRoleAndEntityAndVariable;
            _deleteRolePermissionService = deleteRolePermissionService;
            _updateRolePermissionService = updateRolePermissionService;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("CreateRolePermission")]
        public async Task<CreateRolePermissionResponse> CreateRolePermission(CreateRolePermissionRequest request)
        {
            return await _createRolePermissionService.Execute(request);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetAllPermssions")]
        public async Task<GetAllPermssionsResponse> GetAllPermssions()
        {
            return await _getAllPermissionsService.Execute();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("GetPermissionsByRole")]
        public async Task<GetPermissionsByRoleResponse> GetPermissionsByRole(GetPermissionsByRoleRequest request)
        {
            return await _getPermissionsByRoleService.Execute(request);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("GetPermissionsByRoleAndEntityType")]
        public async Task<GetPermissionsByRoleAndEntityResponse> GetPermissionsByRoleAndEntityType(GetPermissionsByRoleAndEntityRequest request)
        {
            return await _getPermissionsByRoleAndEntityService.Execute(request);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("GetPermissionByRoleAndEntityTypeAndVariableName")]
        public async Task<GetPermissionsByRoleAndEntityAndVariableResponse> GetPermissionByRoleAndEntityTypeAndVariableName(GetPermissionsByRoleAndEntityAndVariableRequest request)
        {
            return await _getPermissionsByRoleAndEntityAndVariableService.Execute(request);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("DeleteRolePermission")]
        public async Task<DeleteRolePermissionResponse> DeleteRolePermission(DeleteRolePermissionRequest request)
        {
            return await _deleteRolePermissionService.Execute(request);
        }
        [AllowAnonymous]
        [HttpPut("UpdateRolePermission/{id}")]
        public async Task<UpdateRolePermissionResponse> UpdateRolePermission(long id, UpdateRolePermissionRequest request)
        {
            return await _updateRolePermissionService.Execute(id, request);
        }


    }
}
