﻿using Application.RolePermission.ChangeRolePermission;
using Application.RolePermission.ChangeRolePermission.Request;
using Application.RolePermission.ChangeRolePermission.Response;
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
    [Authorize(Roles ="ADMIN")]
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
        private readonly ChangeRolePermission _changeRolePermissionService;
        public PermissionsController(CreateRolePermission createRolePermissionService,
            GetAllPermissions getAllPermissionsService,
            GetPermissionsByRole getPermissionsByRoleService,
            GetPermissionsByRoleAndEntity getPermissionsByRoleAndEntityService,
            GetPermissionsByRoleAndEntityAndVariable getPermissionsByRoleAndEntityAndVariable,
            DeleteRolePermission deleteRolePermissionService,
            UpdateRolePermission updateRolePermissionService,
            ChangeRolePermission changeRolePermissionService)
        {
            _createRolePermissionService = createRolePermissionService;
            _getAllPermissionsService = getAllPermissionsService;
            _getPermissionsByRoleService = getPermissionsByRoleService;
            _getPermissionsByRoleAndEntityService = getPermissionsByRoleAndEntityService;
            _getPermissionsByRoleAndEntityAndVariableService = getPermissionsByRoleAndEntityAndVariable;
            _deleteRolePermissionService = deleteRolePermissionService;
            _updateRolePermissionService = updateRolePermissionService;
            _changeRolePermissionService = changeRolePermissionService;

        }
        //[AllowAnonymous]
        [HttpPost]
        [Route("")]
        public async Task<CreateRolePermissionResponse> CreateRolePermission(CreateRolePermissionRequest request)
        {
            return await _createRolePermissionService.Execute(request);
        }

        //[AllowAnonymous]
        [HttpGet]
        [Route("")]
        public async Task<GetAllPermssionsResponse> GetAllPermissions()
        {
            return await _getAllPermissionsService.Execute();
        }

        //[AllowAnonymous]
        [HttpGet]
        [Route("role/{role}")]
        public async Task<GetPermissionsByRoleResponse> GetPermissionsByRole(AccountRole role)
        {
            return await _getPermissionsByRoleService.Execute(new GetPermissionsByRoleRequest() { Role = role });
        }

        //[AllowAnonymous]
        [HttpGet]
        [Route("role/{role}/type/{entityType}")]
        public async Task<GetPermissionsByRoleAndEntityResponse> GetPermissionsByRoleAndEntityType(AccountRole role, EntityType entityType)
        {
            return await _getPermissionsByRoleAndEntityService.Execute(new GetPermissionsByRoleAndEntityRequest() { Role = role, EntityType = entityType });
        }

        //[AllowAnonymous]
        [HttpGet]
        [Route("role/{role}/type/{entityType}/variable/{variableName}")]
        public async Task<GetPermissionsByRoleAndEntityAndVariableResponse> 
            GetPermissionByRoleAndEntityTypeAndVariableName(AccountRole role, EntityType entityType, string variableName)
        {
            return await _getPermissionsByRoleAndEntityAndVariableService
                .Execute(new GetPermissionsByRoleAndEntityAndVariableRequest() {Role = role, EntityType = entityType, 
                    VariableName = variableName});
        }

        //[AllowAnonymous]
        [HttpDelete]
        [Route("{id:long}")]
        public async Task<DeleteRolePermissionResponse> DeleteRolePermission(long id)
        {
            return await _deleteRolePermissionService.Execute(new DeleteRolePermissionRequest() { Id = id });
        }
        
        //[AllowAnonymous]
        [HttpPut("{id:long}")]
        public async Task<UpdateRolePermissionResponse> UpdateRolePermission(long id, 
            UpdateRolePermissionRequest request)
        {
            request.Id = id;
            return await _updateRolePermissionService.Execute(request);
        }
         
        //[AllowAnonymous]
        [HttpPatch("{id:long}/rights/{permission}")]
        public async Task<ChangeRolePermissionResponse> ChangeRolePermission(long id, Permission permission) {

            return await _changeRolePermissionService.Execute(
                new ChangeRolePermissionRequest() {Id = id, NewPermission = permission});
            
        }
        
        

    }
}
