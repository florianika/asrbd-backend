using Application.Ports;
using Application.User.ActivateUser;
using Application.User.ActivateUser.Request;
using Application.User.ActivateUser.Response;
using Application.User.CreateUser;
using Application.User.CreateUser.Request;
using Application.User.CreateUser.Response;
using Application.User.GetAllUsers;
using Application.User.GetAllUsers.Response;
using Application.User.GetUser;
using Application.User.GetUser.Request;
using Application.User.GetUser.Response;
using Application.User.GetUserByEmail;
using Application.User.GetUserByEmail.Request;
using Application.User.GetUserByEmail.Response;
using Application.User.SetUserMunicipality;
using Application.User.SetUserMunicipality.Request;
using Application.User.SetUserMunicipality.Response;
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
    [Authorize(Roles ="ADMIN, SUPERVISOR")]
    [Route("api/admin/users")]
    [ApiController]
    public class UserAdministrationController : AuthControllerBase
    {
        private readonly GetAllUsers _getAllUsersService;
        private readonly GetUser _getUserService;
        private readonly UpdateUserRole _updateUserRoleService;
        private readonly TerminateUser _terminateUserService;
        private readonly ActivateUser _activateUserService;
        private readonly GetUserByEmail _getUserByEmailService;
        private readonly SetUserMunicipality _setUserMunicipalityService;
        private readonly CreateUser _createUserService;
        private readonly IAuthTokenService _authTokenService;

        public UserAdministrationController(GetAllUsers getAllUsersService,
            GetUser getUserService,
            UpdateUserRole updateUserRoleService,
            TerminateUser terminateUserService,
            ActivateUser activateUserService,
            GetUserByEmail getUserByEmailService,
            SetUserMunicipality setUserMunicipalityService,
            CreateUser createUserService,
            IAuthTokenService authTokenService)
        {
            _getAllUsersService = getAllUsersService;
            _getUserService = getUserService;
            _updateUserRoleService = updateUserRoleService;
            _terminateUserService = terminateUserService;
            _activateUserService = activateUserService;
            _getUserByEmailService = getUserByEmailService;
            _setUserMunicipalityService = setUserMunicipalityService;
            _createUserService = createUserService;
            _authTokenService = authTokenService;
        }

        [HttpGet]
        public async Task<GetAllUsersResponse> GetAllUsers()
        {
            var token = ExtractBearerToken();
            var requestUserId = await _authTokenService.GetUserIdFromToken(token);
            var role = await _authTokenService.GetUserRoleFromToken(token);
            var includeAminUsers = Enum.TryParse(role, out AccountRole accountRole) && 
                                   accountRole == AccountRole.ADMIN;
            return await _getAllUsersService.Execute(requestUserId, includeAminUsers);
        }
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<GetUserResponse> GetUser(Guid id)
        {    
            var token = ExtractBearerToken();
            var requestUserId = await _authTokenService.GetUserIdFromToken(token);
            return await _getUserService.Execute(new GetUserRequest {RequestUserId = requestUserId, UserId = id });
        }
        [HttpPost]
        [Route("add")]
        public async Task<CreateUserResponse> CreateUser(CreateUserRequest request)
        {
            var token = ExtractBearerToken();
            var requestUserId = await _authTokenService.GetUserIdFromToken(token);
            request.RequestUserId = requestUserId;
            return await _createUserService.Execute(request);
        }

        [HttpGet]
        [Route("email/{email}")]
        public async Task<GetUserByEmailResponse> GetUserByEmail(string email)
        {
            return await _getUserByEmailService.Execute(new GetUserByEmailRequest() { Email = email });
        }
        
        [HttpPatch]
        [Route("{id:guid}/set/role/{role}")]
        public async Task<UpdateUserRoleResponse> UpdateUserRole(Guid id, AccountRole role)
        {
            var token = ExtractBearerToken();
            var requestUserId = await _authTokenService.GetUserIdFromToken(token);
            return await _updateUserRoleService.Execute(new UpdateUserRoleRequest() { RequestUserId = requestUserId, 
                UserId = id, AccountRole = role});
        }
        
        [HttpPatch]
        [Route("{id:guid}/set/municipality/{municipality}")]
        public async Task<SetUserMunicipalityResponse> SetUserMunicipality(Guid id, string municipality)
        {
            var token = ExtractBearerToken();
            var requestUserId = await _authTokenService.GetUserIdFromToken(token);
            return await _setUserMunicipalityService.Execute(new SetUserMunicipalityRequest() { RequestUserId = requestUserId, 
                UserId = id, MunicipalityCode = municipality});
        }

        [HttpPatch]
        [Route("{id:guid}/terminate")]
        public async Task<TerminateUserResponse> TerminateUser(Guid id)
        {
            var token = ExtractBearerToken();
            var requestUserId = await _authTokenService.GetUserIdFromToken(token);
            return await _terminateUserService.Execute(new TerminateUserRequest() { RequestUserId = requestUserId, 
                UserId = id });
        }

        [HttpPatch]
        [Route("{id:guid}/activate")]
        public async Task<ActivateUserResponse> ActivateUser(Guid id)
        {
            var token = ExtractBearerToken();
            var requestUserId = await _authTokenService.GetUserIdFromToken(token);
            return await _activateUserService.Execute(new ActivateUserRequest() { RequestUserId = requestUserId, 
                UserId = id });
        }
    }
}
