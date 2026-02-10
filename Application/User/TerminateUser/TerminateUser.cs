
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
            ValidateUserTermination(request);
            if (request.RequestUserRole != null)
                await TerminateUserAsync(request.UserId.ToString(), request.RequestUserRole);
            return new TerminateUserSuccessResponse
            {
                Message = "User terminated."
            };
        }

        private void ValidateUserTermination(TerminateUserRequest request)
        {
            try
            {
                if (request.UserId == request.RequestUserId)
                    throw new ForbidenException("Cannot terminate yourself.");

                Enum.TryParse(request.RequestUserRole, out AccountRole accountRole);
                if (accountRole is not (AccountRole.ADMIN or AccountRole.SUPERVISOR))
                    throw new ForbidenException("Cannot terminate user.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw; 
            }
        }

        private async Task TerminateUserAsync(string userId, string requestUserRole)
        {
            try
            {
                await _authRepository.UpdateAccountUser(new Guid(userId), AccountStatus.TERMINATED, requestUserRole);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
            
        }
    }
}
