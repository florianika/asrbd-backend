using Application.User.CreateUser;
using Application.User.CreateUser.Request;
using Application.User.CreateUser.Response;
using Application.User.Login;
using Application.User.Login.Request;
using Application.User.Login.Response;
using Application.User.RefreshToken;
using Application.User.RefreshToken.Request;
using Application.User.RefreshToken.Response;
using Application.User.SignOut;
using Application.User.SignOut.Request;
using Application.User.SignOut.Response;
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
        public AuthController(CreateUser createUserService, Login loginService, RefreshToken refreshTokenService, SignOut signOutService)
        {
            _createUserService = createUserService;
            _loginservice = loginService;
            _refreshTokenService = refreshTokenService;
            _signOutService = signOutService;
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


        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [AllowAnonymous]
        [HttpGet]
        [Route("Koha")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 3).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }


    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }
}
