using Application.Configuration;
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
using Application.User.GetUser;
using Application.User.GetUser.Response;
using Application.User.GetUser.Request;
using Domain.Enum;
using Application.Exceptions;
using Application.User.GetUserByEmail;
using Application.User.GetUserByEmail.Request;
using Application.User.GetUserByEmail.Response;
using Application.User.SetUserMunicipality;
using Application.User.SetUserMunicipality.Request;
using Application.User.SetUserMunicipality.Response;
using Microsoft.Extensions.Options;
using Application.Queries;
using Application.Queries.GetMunicipalities.Response;
using Application.User.Login2fa.Response;
using Application.User.Login2fa.Request;
using Application.User.Login2fa;
using Application.User.Verify2fa.Request;
using Application.User.Verify2fa;
using Application.User.Verify2fa.Response;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        
        private readonly CreateUser _createUserService;
        private readonly Login _loginService;
        private readonly Login2fa _login2faService;
        private readonly Verify2fa _verify2faService;
        private readonly RefreshToken _refreshTokenService;
        private readonly SignOut _signOutService;
        private readonly GetAllUsers _getAllUsersService;
        private readonly UpdateUserRole _updateUserRoleService;
        private readonly TerminateUser _terminateUserService;
        private readonly ActivateUser _activateUserService;
        private readonly GetUser _getUserService;
        private readonly GetUserByEmail _getUserByEmailService;
        private readonly IOptions<GisServerCredentials> _gisServerCredentials;
        private readonly IOptions<GisFormRequest> _gisFormRequest;
        private readonly SetUserMunicipality _setUserMunicipalityService;
        private readonly IGetMunicipalitiesQuery _getMunicipalitiesQuery;
        private readonly IHttpClientFactory _httpClientFactory;
        public AuthController(CreateUser createUserService, 
            Login loginService,

            RefreshToken refreshTokenService,
            SignOut signOutService,
            GetAllUsers getAllUsersService,
            UpdateUserRole updateUserRoleService,
            TerminateUser terminateUserService,
            ActivateUser activateUserService,
            GetUser getUserService,
            GetUserByEmail getUserByEmailService,
            IOptions<GisServerCredentials> gisServerCredentials,
            IOptions<GisFormRequest> gisFormRequest,
            SetUserMunicipality setUserMunicipalityService,
            IGetMunicipalitiesQuery getMunicipalitiesQuery,
            IHttpClientFactory httpClientFactory,
            Login2fa login2faService,
            Verify2fa verify2faService)
        {
            _createUserService = createUserService;
            _loginService = loginService;
            _refreshTokenService = refreshTokenService;
            _signOutService = signOutService;
            _getAllUsersService = getAllUsersService;
            _updateUserRoleService = updateUserRoleService;
            _terminateUserService = terminateUserService;
            _activateUserService = activateUserService;
            _getUserService = getUserService;
            _getUserByEmailService = getUserByEmailService;
            _gisServerCredentials = gisServerCredentials;
            _gisFormRequest = gisFormRequest;
            _setUserMunicipalityService = setUserMunicipalityService;
            _getMunicipalitiesQuery = getMunicipalitiesQuery;
            _httpClientFactory = httpClientFactory;
            _login2faService = login2faService;
            _verify2faService = verify2faService;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("signup")]
        public async Task<CreateUserResponse> CreateUser(CreateUserRequest request)
        {
            return await _createUserService.Execute(request);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<LoginResponse> Login(LoginRequest request)
        {
            return await _loginService.Execute(request);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("2fa/login")]
        public async Task<Login2faResponse> Login2fa(Login2faRequest request)
        {
            return await _login2faService.Execute(request);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("2fa/verify")]
        public async Task<Verify2faResponse> Verify2fa(Verify2faRequest request)
        {
            return await _verify2faService.Execute(request);
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("RefreshToken")]
        public async Task<RefreshTokenResponse> RefreshToken(RefreshTokenRequest request)
        {
            return await _refreshTokenService.Execute(request);
        }

        //[AllowAnonymous]
        [HttpPost]
        [Route("signout")]
        public async Task<SignOutResponse> SignOut(SignOutRequest request)
        {
            return await _signOutService.Execute(request);
        }

        //[AllowAnonymous]
        [HttpGet]
        [Route("users")]
        public async Task<GetAllUsersResponse> GetAllUsers()
        {
            return await _getAllUsersService.Execute();
        }

        //[AllowAnonymous]
        [HttpPatch]
        [Route("users/{id:guid}/set/{role}")]
        public async Task<UpdateUserRoleResponse> UpdateUserRole(Guid id, string role)
        {
            if (Enum.TryParse(role, out AccountRole accountRole))
            {
                return await _updateUserRoleService.Execute(new UpdateUserRoleRequest() { UserId = id, AccountRole = accountRole });
            }
            throw new EnumExeption("Invalid Role");
        }
        
        [HttpPatch]
        [Route("users/{id:guid}/set/municipality/{municipalityCode}")]
        public async Task<SetUserMunicipalityResponse> SetUserMunicipality(Guid id, string municipalityCode)
        {
            return await _setUserMunicipalityService.Execute(new SetUserMunicipalityRequest()
            {
                UserId = id, 
                MunicipalityCode = municipalityCode
            });
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("users/municipalities")]
        public async Task<GetMunicipalitiesResponse> GetMunicipalities()
        {
            return await _getMunicipalitiesQuery.Execute();
        }

        //[AllowAnonymous]
        [HttpPatch]
        [Route("users/{id:guid}/terminate")]
        public async Task<TerminateUserResponse> TerminateUser(Guid id)
        {
            return await _terminateUserService.Execute(new TerminateUserRequest() { UserId = id });
        }

        //[AllowAnonymous]
        [HttpPatch]
        [Route("users/{id:guid}/activate")]
        public async Task<ActivateUserResponse> ActivateUser(Guid id)
        {
            return await _activateUserService.Execute(new ActivateUserRequest() { UserId = id });
        }

        //[AllowAnonymous]
        [HttpGet]
        [Route("users/{id:guid}")]
        public async Task<GetUserResponse> GetUser(Guid id)
        {
            return await _getUserService.Execute(new GetUserRequest() { UserId = id });
        }
        
        //[AllowAnonymous]
        [HttpGet]
        [Route("users/email/{email}")]
        public async Task<GetUserByEmailResponse> GetUserByEmail(string email)
        {
            return await _getUserByEmailService.Execute(new GetUserByEmailRequest() { Email = email });
        }
        
        //[AllowAnonymous]
        [HttpGet]
        [Route("gis/credentials")]
        public GisServerCredentials GetGisServerCredentials()
        {
            return _gisServerCredentials.Value;
        }
        
        //[AllowAnonymous]
        [HttpGet]
        [Route("gis/login")]
        public async Task<GisLoginResponse?> GisLogin()
        {
            var client = _httpClientFactory.CreateClient("gis");
            var frmUrl = new FormUrlEncodedContent(_gisFormRequest.Value);
            var httpResponse = await client.PostAsync("/portal/sharing/rest/generateToken", frmUrl);
            var response = await httpResponse.Content.ReadFromJsonAsync<GisLoginResponse>();
            return response;

        }
    }
}
