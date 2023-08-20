using Application.Enums;
using Application.User;
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
using Domain.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize(Roles ="ADMIN")]
    [Route("api/admin/")]
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
        [Route("users")]
        public async Task<GetAllUsersResponse> GetAllUsers()
        {
            return await _getAllUsersService.Execute();
        }
        [HttpGet]
        [Route("user/{guid}")]
        public async Task<GetUserResponse> GetUser(Guid guid)
        {
            GetUserRequest request = new GetUserRequest { UserId = guid };          
            return await _getUserService.Execute(request);
        }
        [HttpPatch]
        [Route("user/{guid}")]
        public async Task<UpdateUserRoleResponse> UpdateUserRole(Guid guid, UpdateUserRoleRequest request)
        {
            if (guid != request.UserId)
                return new UpdateUserRoleErrorResponse
                {
                    Message = Enum.GetName(ErrorCodes.BadRequest),
                    Code = ErrorCodes.BadRequest.ToString("D")
                };
            else
                return await _updateUserRoleService.Execute(request);
        }

        [HttpPatch]
        [Route("admin/terminate/user/{guid}")]
        public async Task<TerminateUserResponse> TerminateUser(Guid guid, TerminateUserRequest request)
        {
            if (guid != request.UserId)
                return new TerminateUserErrorResponse
                {
                    Message = Enum.GetName(ErrorCodes.BadRequest),
                    Code = ErrorCodes.BadRequest.ToString("D")
                };
            else
                return await _terminateUserService.Execute(request);
        }

        [HttpPatch]
        [Route("admin/activate/user/{guid}")]
        public async Task<ActivateUserResponse> ActivateUser(Guid guid, ActivateUserRequest request)
        {
            if (guid != request.UserId)
                return new ActivateUserErrorResponse
                {
                    Message = Enum.GetName(ErrorCodes.BadRequest),
                    Code = ErrorCodes.BadRequest.ToString("D")
                };
            else
                return await _activateUserService.Execute(request);
        }
    }
}
