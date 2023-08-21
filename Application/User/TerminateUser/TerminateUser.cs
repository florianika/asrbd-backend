
using Application.Enums;
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
                var userExists = await _authRepository.CheckIfUserExists(request.UserId);
                if (userExists == false)
                {
                    return new TerminateUserErrorResponse
                    {
                        Message = Enum.GetName(ErrorCodes.UserDoesNotExist),
                        Code = ErrorCodes.UserDoesNotExist.ToString("D")
                    };
                }
                await _authRepository.UpdateAccountUser(request.UserId, AccountStatus.TERMINATED);
                return new TerminateUserSuccessResponse
                {
                    Message = "User terminated."
                };
               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new TerminateUserErrorResponse
                {
                    Code = ErrorCodes.AnUnexpectedErrorOcurred.ToString("D"),
                    Message = Enum.GetName(ErrorCodes.AnUnexpectedErrorOcurred)
                };
            }
        }
    }
}
