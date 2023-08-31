
using Application.Enums;
using Application.Exceptions;
using Application.Ports;
using Application.User.Login.Request;
using Application.User.Login.Response;
using Domain.Enum;
using Microsoft.Extensions.Logging;

namespace Application.User.Login
{
    public class Login : ILogin
    {
        private readonly ILogger _logger;
        private readonly IAuthRepository _authRepository;
        private readonly ICryptographyService _cryptographyService;
        private readonly IAuthTokenService _authTokenService;
        public Login(ILogger<Login> logger,
            IAuthRepository authRepository,
            ICryptographyService cryptographyService, IAuthTokenService authTokenService)
        {
            _logger= logger;
            _authRepository = authRepository;
            _cryptographyService = cryptographyService;
            _authTokenService = authTokenService;
        }

        public async Task<LoginResponse> Execute(LoginRequest request)
        {
            var user = await _authRepository.FindUserByEmail(request.Email);
            if (user.AccountStatus == AccountStatus.LOCKED && user.LockExpiration > DateTime.Now)
            {
                throw new ForbidenException("Account locked");
            }

            ValidatePassword(request.Password, user);
                
            await UpdateUserAfterSuccessfulLogin(user);
            var (idToken, accessToken) = await GenerateTokens(user);
            var response = new LoginSuccessResponse
            {
                IdToken = idToken,
                AccessToken = accessToken,
                RefreshToken = user.RefreshToken.Value
            };
            return response;
        }

             
        private async Task UpdateUserAfterSuccessfulLogin(Domain.User user)
        {
            user.RefreshToken = new Domain.RefreshToken
            {
                UserId = user.Id,
                Value = await _authTokenService.GenerateRefreshToken(),
                Active = true,
                ExpirationDate = DateTime.Now.AddMinutes(await _authTokenService.GetRefreshTokenLifetimeInMinutes())
            };
            await _authRepository.UpdateRefreshToken(user.Id, user.RefreshToken);
            await _authRepository.UnlockAccount(user);
        }
        private async Task<(string idToken, string accessToken)> GenerateTokens(Domain.User user)
        {
            var idToken = await _authTokenService.GenerateIdToken(user);
            var accessToken = await _authTokenService.GenerateAccessToken(user);
            return (idToken, accessToken);
        }
        private void ValidatePassword(string testPassword, Domain.User user)
        {
            var hash = _cryptographyService.HashPassword(testPassword, user.Salt);
            if (hash != user.Password) {
                throw new ForbidenException("Email or password wrong");
            }
        }
    }
}
