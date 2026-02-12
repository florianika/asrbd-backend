using Application.Exceptions;
using Application.Ports;
using Application.User.TerminateUser.Request;
using Application.User.TerminateUser.Response;
using Domain.Enum;
using Microsoft.Extensions.Logging;

namespace Application.User.TerminateUser
{
    public class TerminateUser : ITerminateUser
    {
        private readonly ILogger _logger;
        private readonly IAuthRepository _authRepository;
        public TerminateUser(ILogger<TerminateUser> logger,
            IAuthRepository authRepository)
        {
            _logger = logger;
            _authRepository = authRepository;
        }
        
        public async Task<TerminateUserResponse> Execute(TerminateUserRequest request)
        {
            try
            {
                if (request.UserId == request.RequestUserId)
                    throw new ForbidenException("Cannot terminate yourself.");
                await _authRepository.UpdateAccountUser(request.UserId, AccountStatus.TERMINATED, 
                    request.RequestUserRole);
                return new TerminateUserSuccessResponse
                {
                    Message = "User terminated."
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
