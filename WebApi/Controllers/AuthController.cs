using Application.User.CreateUser;
using Application.User.CreateUser.Request;
using Application.User.CreateUser.Response;
using Application.User.GetAllUsers.Response;
using Application.User.Login;
using Application.User.Login.Request;
using Application.User.Login.Response;
using Application.User.RefreshToken;
using Application.User.RefreshToken.Request;
using Application.User.RefreshToken.Response;
using Application.User.SignOut;
using Application.User.SignOut.Request;
using Application.User.SignOut.Response;
using Application.User.GetAllUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.User.UpdateUserRole.Request;
using Application.User.UpdateUserRole.Response;
using Application.User.UpdateUserRole;
using Application.User.TerminateUser;
using Application.User.TerminateUser.Request;
using Application.User.TerminateUser.Response;
using Application.User.ActivateUser.Request;
using Application.User.ActivateUser.Response;
using Application.User.ActivateUser;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly CreateUser _createUserService;
        private readonly Login _loginservice;
        private readonly RefreshToken _refreshTokenService;
        private readonly SignOut _signOutService;
        private readonly GetAllUsers _getAllUsersService;
        private readonly UpdateUserRole _updateUserRoleService;
        private readonly TerminateUser _terminateUserService;
        private readonly ActivateUser _activateUserService;
        public AuthController(CreateUser createUserService, 
            Login loginService,
            RefreshToken refreshTokenService,
            SignOut signOutService,
            GetAllUsers getAllUsersService,
            UpdateUserRole updateUserRoleService,
            TerminateUser terminateUserService,
            ActivateUser activateUserService)
        {
            _createUserService = createUserService;
            _loginservice = loginService;
            _refreshTokenService = refreshTokenService;
            _signOutService = signOutService;
            _getAllUsersService = getAllUsersService;
            _updateUserRoleService = updateUserRoleService;
            _terminateUserService = terminateUserService;
            _activateUserService = activateUserService;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("CreateUser")]
        public async Task<CreateUserResponse> CreateUser(CreateUserRequest request)
        {
            return await _createUserService.Execute(request);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public async Task<LoginResponse> Login(LoginRequest request)
        {
            return await _loginservice.Execute(request);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("RefreshToken")]
        public async Task<RefreshTokenResponse> RefreshToken(RefreshTokenRequest request)
        {
            return await _refreshTokenService.Execute(request);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("SignOut")]
        public async Task<SignOutResponse> SignOut(SignOutRequest request)
        {
            return await _signOutService.Execute(request);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<GetAllUsersResponse> GetAllUsers()
        {
            return await _getAllUsersService.Execute();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("UpdateUserRole")]
        public async Task<UpdateUserRoleResponse> UpdateUserRole(UpdateUserRoleRequest request)
        {
            return await _updateUserRoleService.Execute(request);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("TeminateUser")]
        public async Task<TerminateUserResponse> TerminateUser(TerminateUserRequest request)
        {
            return await _terminateUserService.Execute(request);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("ActivateUser")]
        public async Task<ActivateUserResponse> ActivateUser(ActivateUserRequest request)
        {
            return await _activateUserService.Execute(request);
        }
    }
}
