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
using Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/admin/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private readonly CreateRolePermission _createRolePermissionService;
        private readonly GetAllPermissions _getAllPermissionsService;
        private readonly GetPermissionsByRole _getPermissionsByRoleService;
        private readonly GetPermissionsByRoleAndEntity _getPermissionsByRoleAndEntityService;
        private readonly GetPermissionsByRoleAndEntityAndVariable _getPermissionsByRoleAndEntityAndVariableService;
        private readonly DeleteRolePermission _deleteRolePermissionService;
        private readonly UpdateRolePermission _updateRolePermissionService;
        public PermissionsController(CreateRolePermission createRolePermissionService,
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
        [Route("")]
        public async Task<CreateRolePermissionResponse> CreateRolePermission(CreateRolePermissionRequest request)
        {
            return await _createRolePermissionService.Execute(request);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("")]
        public async Task<GetAllPermssionsResponse> GetAllPermssions()
        {
            return await _getAllPermissionsService.Execute();
        }

        [AllowAnonymous]
        [HttpGet]
        //FIXME ther route here should be /api/PermissionConroller/role/{role}
        [Route("role/{role}")]
        public async Task<GetPermissionsByRoleResponse> GetPermissionsByRole(AccountRole role)
        {
            return await _getPermissionsByRoleService.Execute(new GetPermissionsByRoleRequest() { Role = role });
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("role/{role}/type/{entityType}")]
        public async Task<GetPermissionsByRoleAndEntityResponse> GetPermissionsByRoleAndEntityType(AccountRole role, EntityType entityType)
        {
            return await _getPermissionsByRoleAndEntityService.Execute(new GetPermissionsByRoleAndEntityRequest() { Role = role, EntityType = entityType });
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("role/{role}/type/{enityType}/variable/{variableName}")]
        public async Task<GetPermissionsByRoleAndEntityAndVariableResponse> GetPermissionByRoleAndEntityTypeAndVariableName(AccountRole role, EntityType entityType, string variableName)
        {
            return await _getPermissionsByRoleAndEntityAndVariableService.Execute(new GetPermissionsByRoleAndEntityAndVariableRequest() {Role = role, EntityType = entityType, VariableName = variableName});
        }

        [AllowAnonymous]
        [HttpDelete]
        [Route("{id}")]
        public async Task<DeleteRolePermissionResponse> DeleteRolePermission(long id)
        {
            return await _deleteRolePermissionService.Execute(new DeleteRolePermissionRequest() { Id = id });
        }
        [AllowAnonymous]
        [HttpPut("{id}")]
        public async Task<UpdateRolePermissionResponse> UpdateRolePermission(long id, UpdateRolePermissionRequest request)
        {
            return await _updateRolePermissionService.Execute(id, request);
        }


    }
}
