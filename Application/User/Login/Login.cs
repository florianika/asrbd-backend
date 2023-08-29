
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

        //FIXME It took me a while to understand this method, this is too long and needs refactoring
        //I think you should not cerate the error response here but throw different exceptions then deal with exceptions that creates
        //error messages according to the exceptions rised. This will shorten the method a lot 
        //Then you should add some private methods to call, like refreshToken(user), succesfulLogin(user)
        //Try to do what you think makes this method more clean, then we can discuss it together no problem.
        public async Task<LoginResponse> Execute(LoginRequest request)
        {
            try
            {
                var user = await GetUserByEmailOrThrow(request.Email);
                if (!IsUserActive(user))
                {
                    HandleInactiveUser(user);
                }
                if (AreCredentialsValid(request.Password, user))
                {
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
                else
                {
                    HandleInvalidCredentials(user);
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        private void HandleInactiveUser(Domain.User.User user)
        {
            if (user.AccountStatus == AccountStatus.LOCKED && user.LockExpiration > DateTime.Now)
            {
                throw new Application.Exceptions.ForbidenException("Account locked");
            }
        }

        private void HandleInvalidCredentials(Domain.User.User user)
        {
            if (user.AccountStatus == AccountStatus.LOCKED && user.LockExpiration > DateTime.Now)
            {
                throw new Application.Exceptions.ForbidenException("Account locked");
            }
        }

        private async Task<Domain.User.User> GetUserByEmailOrThrow(string email)
        {
            var user = await _authRepository.FindUserByEmail(email);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            return user;
        }
        private bool IsUserActive(Domain.User.User user)
        {
            return user.AccountStatus == AccountStatus.ACTIVE;
        }       
        private async Task UpdateUserAfterSuccessfulLogin(Domain.User.User user)
        {
            user.RefreshToken = new Domain.RefreshToken.RefreshToken
            {
                UserId = user.Id,
                Value = await _authTokenService.GenerateRefreshToken(),
                Active = true,
                ExpirationDate = DateTime.Now.AddMinutes(await _authTokenService.GetRefreshTokenLifetimeInMinutes())
            };
            await _authRepository.UpdateRefreshToken(user.Id, user.RefreshToken);
            await _authRepository.UnlockAccount(user);
        }
        private async Task<(string idToken, string accessToken)> GenerateTokens(Domain.User.User user)
        {
            var idToken = await _authTokenService.GenerateIdToken(user);
            var accessToken = await _authTokenService.GenerateAccessToken(user);
            return (idToken, accessToken);
        }
        private bool AreCredentialsValid(string testPassword, Domain.User.User user)
        {
            var hash = _cryptographyService.HashPassword(testPassword, user.Salt);
            return hash == user.Password;
        }
    }
}
