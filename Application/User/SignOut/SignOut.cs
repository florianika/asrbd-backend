using Application.Ports;
using Application.User.SignOut.Request;
using Application.User.SignOut.Response;
using Microsoft.Extensions.Logging;

namespace Application.User.SignOut
{
    public class SignOut : ISignOut
    {
        private readonly ILogger _logger;
        private readonly IAuthRepository _authRepository;
        public SignOut(ILogger<SignOut> logger,
            IAuthRepository authRepository)
        {
            _logger = logger;
            _authRepository = authRepository;
        }
        public async Task<SignOutResponse> Execute(SignOutRequest request)
        {
            try
            {
                var user = await _authRepository.GetUserByUserId(request.UserId);
                if (user == null)
                {
                    return new SignOutErrorResponse { Message = "User not found" };
                }

                // Kontrollo nëse RefreshToken është null
                if (user.RefreshToken == null)
                {
                    return new SignOutErrorResponse { Message = "No refresh token found for the user" };
                }
                user.RefreshToken.Active = false;
                await _authRepository.UpdateRefreshToken(user.Id, user.RefreshToken);

                return new SignOutSuccessResponse
                {
                    Message = $"User signed out at {DateTime.Now} server time."
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
