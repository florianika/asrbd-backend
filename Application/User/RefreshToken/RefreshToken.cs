using Application.Exceptions;
using Application.Ports;
using Application.User.RefreshToken.Request;
using Application.User.RefreshToken.Response;
using Microsoft.Extensions.Logging;


namespace Application.User.RefreshToken
{
    public class RefreshToken : IRefreshToken
    {
        private readonly ILogger _logger;
        private readonly IAuthTokenService _authTokenService;
        private readonly IAuthRepository _authRepository;
        public RefreshToken(
            ILogger<RefreshToken> logger,
            IAuthTokenService authTokenService,
            IAuthRepository authRepository)
        {
            _logger = logger;
            _authTokenService = authTokenService;
            _authRepository = authRepository;
        }
        public async Task<RefreshTokenResponse> Execute(RefreshTokenRequest request)
        {
            try
            {
                if (!await _authTokenService.IsTokenValid(request.AccessToken, false))
                {
                    throw new InvalidTokenException("Invalid token");
                }

                var userId = await _authTokenService.GetUserIdFromToken(request.AccessToken);
                var user = await _authRepository.GetUserByUserId(userId);
                
                ValidateToken(user.RefreshToken, request);
                
                var idToken = await _authTokenService.GenerateIdToken(user);
                var newToken = await _authTokenService.GenerateAccessToken(user);

                await UpdateRefreshToken(user.Id, user.RefreshToken);

                var response = new RefreshTokenSuccessResponse
                {
                    IdToken = idToken,
                    AccessToken = newToken,
                    RefreshToken = user.RefreshToken.Value,
                    RefreshTokenExpirationDate = user.RefreshToken.ExpirationDate
                };

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        private static void ValidateToken(Domain.RefreshToken refreshToken, RefreshTokenRequest request) 
        {
            if (!refreshToken.Active)
            {
                throw new InvalidTokenException("Inactive token");
            }

            if (refreshToken.ExpirationDate < DateTime.Now)
            {
                throw new InvalidTokenException("Expired token");
            }

            if (refreshToken.Value != request.RefreshToken)
            {
                throw new InvalidTokenException("Invalid token");
            }

        }
        private async Task UpdateRefreshToken(Guid userId, Domain.RefreshToken refreshToken)
        {
            refreshToken.Value = await _authTokenService.GenerateRefreshToken();
            refreshToken.Active = true;
            refreshToken.ExpirationDate = DateTime.Now.AddMinutes(await _authTokenService.GetRefreshTokenLifetimeInMinutes());
            await _authRepository.UpdateRefreshToken(userId, refreshToken);
        }
    }
}
