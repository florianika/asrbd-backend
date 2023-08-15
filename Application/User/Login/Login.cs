
using Application.Enums;
using Application.Ports;
using Application.User.Login.Request;
using Application.User.Login.Response;
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
            try
            {
                var user = await _authRepository.GetUserByEmail(request.Email);
                if (user == null)
                {
                    var response = new LoginErrorResponse
                    {
                        Message = Enum.GetName(ErrorCodes.UserDoesNotExist),
                        Code = ErrorCodes.UserDoesNotExist.ToString("D")
                    };
                    return response;
                }
                if (user.AccountStatus != Domain.User.AccountStatus.ACTIVE)
                {
                    var response = new LoginErrorResponse
                    {
                        Message = Enum.GetName(ErrorCodes.AccountStatusNotActive),
                        Code = ErrorCodes.AccountStatusNotActive.ToString("D")
                    };
                    return response;
                }

                if (AreCredentialsValid(request.Password, user))
                {
                    user.RefreshToken = new Domain.RefreshToken.RefreshToken
                    {
                        UserId = user.Id,
                        Value = await _authTokenService.GenerateRefreshToken(),
                        Active = true,
                        ExpirationDate = DateTime.Now.AddMinutes(await _authTokenService.GetRefreshTokenLifetimeInMinutes())
                    };
                    await _authRepository.UpdateRefreshToken(user.Id, user.RefreshToken);

                    var idToken = await _authTokenService.GenerateIdToken(user);
                    var accessToken = await _authTokenService.GenerateAccessToken(user);

                    var response = new LoginSuccessResponse
                    {
                        IdToken = idToken,
                        AccessToken = accessToken,
                        RefreshToken = user.RefreshToken.Value
                    };

                    return response;
                }
                else
                {
                    var response = new LoginErrorResponse
                    {
                        Message = Enum.GetName(ErrorCodes.CredentialsAreNotValid),
                        Code = ErrorCodes.CredentialsAreNotValid.ToString("D")
                    };

                    return response;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                var response = new LoginErrorResponse
                {
                    Message = Enum.GetName(ErrorCodes.AnUnexpectedErrorOcurred),
                    Code = ErrorCodes.AnUnexpectedErrorOcurred.ToString("D")
                };

                return response;
            }
        }

        private bool AreCredentialsValid(string testPassword, Domain.User.User user)
        {
            var hash = _cryptographyService.HashPassword(testPassword, user.Salt);
            return hash == user.Password;
        }
    }
}
