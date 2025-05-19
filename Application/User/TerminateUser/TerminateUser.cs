
using Application.Enums;
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
            await TerminateUserAsync(request.UserId.ToString());
            return new TerminateUserSuccessResponse
            {
                Message = "User terminated."
            };
        }

        private async Task TerminateUserAsync(string userId)
        {
            try
            {
                await _authRepository.UpdateAccountUser(new Guid(userId), AccountStatus.TERMINATED);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
            
        }
    }
}
