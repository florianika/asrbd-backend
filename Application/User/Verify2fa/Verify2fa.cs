using Application.Exceptions;
using Application.Ports;
using Application.User.Login.Request;
using Application.User.Login.Response;
using Application.User.Verify2fa.Request;
using Application.User.Verify2fa.Response;
using Domain;
using Microsoft.Extensions.Logging;

namespace Application.User.Verify2fa
{
    public class Verify2fa : IVerify2fa
    {
        private readonly ILogger _logger;
        private readonly IAuthRepository _authRepository;
        private readonly ICryptographyService _cryptographyService;
        private readonly IAuthTokenService _authTokenService;
        private readonly IOtpRepository _otpRepository;

        public Verify2fa(ILogger<Verify2fa> logger, IAuthRepository authRepository, ICryptographyService cryptographyService, IAuthTokenService authTokenService, IOtpRepository otpRepository)
        {
            _logger = logger;
            _authRepository = authRepository;
            _cryptographyService = cryptographyService;
            _authTokenService = authTokenService;
            _otpRepository = otpRepository;
        }
        public async Task<Verify2faResponse> Execute(Verify2faRequest request)
        {
            var ok = await _otpRepository.VerifyOtp(request.UserId, request.Code);
            if (!ok) throw new ForbidenException("Invalid or expired code");

            var user = await _authRepository.FindUserById(request.UserId);
            await UpdateUserAfterSuccessfulLogin(user);

            var (idToken, accessToken) = await GenerateTokens(user);

            var response = new Verify2faSuccessResponse
            {
                IdToken = idToken,
                AccessToken = accessToken,
                RefreshToken = user.RefreshToken.Value
            };
            return response;
        }
        private async Task UpdateUserAfterSuccessfulLogin(Domain.User user)
        {
            user.RefreshToken ??= new Domain.RefreshToken();
            user.RefreshToken.Active = true;
            user.RefreshToken.ExpirationDate = DateTime.Now.AddMinutes(await _authTokenService.GetRefreshTokenLifetimeInMinutes());
            user.RefreshToken.Value = await _authTokenService.GenerateRefreshToken();

            await _authRepository.UpdateRefreshToken(user.Id, user.RefreshToken);
            await _authRepository.UnlockAccount(user);
        }
        private async Task<(string idToken, string accessToken)> GenerateTokens(Domain.User user)
        {
            var idToken = await _authTokenService.GenerateIdToken(user);
            var accessToken = await _authTokenService.GenerateAccessToken(user);
            return (idToken, accessToken);
        }
    }
}
