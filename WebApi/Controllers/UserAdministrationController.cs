using Application.User.ActivateUser;
using Application.User.ActivateUser.Request;
using Application.User.ActivateUser.Response;
using Application.User.GetAllUsers;
using Application.User.GetAllUsers.Response;
using Application.User.GetUser;
using Application.User.GetUser.Request;
using Application.User.GetUser.Response;
using Application.User.TerminateUser;
using Application.User.TerminateUser.Request;
using Application.User.TerminateUser.Response;
using Application.User.UpdateUserRole;
using Application.User.UpdateUserRole.Request;
using Application.User.UpdateUserRole.Response;
using Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize(Roles ="ADMIN")]
    [Route("api/admin/users")]
    [ApiController]
    public class UserAdministrationController : ControllerBase
    {
        private readonly GetAllUsers _getAllUsersService;
        private readonly GetUser _getUserService;
        private readonly UpdateUserRole _updateUserRoleService;
        private readonly TerminateUser _terminateUserService;
        private readonly ActivateUser _activateUserService;
        public UserAdministrationController(GetAllUsers getAllUsersService,
            GetUser getUserService,
            UpdateUserRole updateUserRoleService,
            TerminateUser terminateUserService,
            ActivateUser activateUserService)
        {
            _getAllUsersService = getAllUsersService;
            _getUserService = getUserService;
            _updateUserRoleService = updateUserRoleService;
            _terminateUserService = terminateUserService;
            _activateUserService = activateUserService;
        }

        [HttpGet]
        public async Task<GetAllUsersResponse> GetAllUsers()
        {
            return await _getAllUsersService.Execute();
        }
        [HttpGet]
        [Route("/{guid:guid}")]
        public async Task<GetUserResponse> GetUser(Guid guid)
        {    
            return await _getUserService.Execute(new GetUserRequest { UserId = guid });
        }
        [HttpPatch]
        [Route("/{guid:guid}/set/role/{role}")]
        public async Task<UpdateUserRoleResponse> UpdateUserRole(Guid guid, AccountRole role)
        {
            return await _updateUserRoleService.Execute(new UpdateUserRoleRequest() { UserId = guid, AccountRole = role});
        }

        [HttpPatch]
        [Route("/{guid:guid}/terminate")]
        public async Task<TerminateUserResponse> TerminateUser(Guid guid)
        {
            return await _terminateUserService.Execute(new TerminateUserRequest() {UserId = guid });
        }

        [HttpPatch]
        [Route("/{guid:guid}/activate")]
        public async Task<ActivateUserResponse> ActivateUser(Guid guid)
        {
            return await _activateUserService.Execute(new ActivateUserRequest() { UserId = guid });
        }
    }
}
