
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
        //FIXME refactor and separate error handling
        public async Task<TerminateUserResponse> Execute(TerminateUserRequest request)
        {
            try
            {
                await TerminateUserAsync(request.UserId.ToString());
                return new TerminateUserSuccessResponse
                {
                    Message = "User terminated."
                };
            }
            catch (Exception ex)
            {
                // Logging can be added here if needed
                throw;
            }
        }
        private async Task TerminateUserAsync(string userId)
        {
            var userExists = await _authRepository.CheckIfUserExists(new Guid(userId));
            if (!userExists)
            {
                throw new NotFoundException("User not found");
            }
            await _authRepository.UpdateAccountUser(new Guid(userId), AccountStatus.TERMINATED);
        }
    }
}
