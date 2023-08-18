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
        public AuthController(CreateUser createUserService, 
            Login loginService, 
            RefreshToken refreshTokenService, 
            SignOut signOutService,
            GetAllUsers getAllUsersService)
        {
            _createUserService = createUserService;
            _loginservice = loginService;
            _refreshTokenService = refreshTokenService;
            _signOutService = signOutService;
            _getAllUsersService = getAllUsersService;
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




        //[AllowAnonymous]
        //[HttpGet]
        //[Route("ActivateAccount")]
        //public async Task<GetRolesResponse> Get()
        //{
        //    return await _getRolesService.Execute();
        //}
    }
}
