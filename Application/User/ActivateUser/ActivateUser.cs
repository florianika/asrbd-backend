using Application.Exceptions;
using Application.Ports;
using Application.User.ActivateUser.Request;
using Application.User.ActivateUser.Response;
using Domain.Enum;
using Microsoft.Extensions.Logging;

namespace Application.User.ActivateUser
{
    public class ActivateUser : IActivateUser
    {
        private readonly ILogger _logger;
        private readonly IAuthRepository _authRepository;
        public ActivateUser(ILogger<ActivateUser> logger,
            IAuthRepository authRepository)
        {
            _logger = logger;
            _authRepository = authRepository;
        }
        public async Task<ActivateUserResponse> Execute(ActivateUserRequest request)
        {
            try
            {
                if (request.UserId == request.RequestUserId)
                    throw new ForbidenException("Cannot activate yourself.");
                await _authRepository.UpdateAccountUser(request.UserId, AccountStatus.ACTIVE, 
                    request.RequestUserRole);
                return new ActivateUserSuccessResponse
                {
                    Message = "User activated."
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
