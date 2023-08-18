using Application.Enums;
using Application.Ports;
using Application.User.ActivateUser.Request;
using Application.User.ActivateUser.Response;
using Application.User.TerminateUser.Response;
using Domain.User;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                var userExists = await _authRepository.CheckIfUserExists(request.UserId);
                if (userExists == false)
                {
                    return new ActivateUserErrorResponse
                    {
                        Message = Enum.GetName(ErrorCodes.UserDoesNotExist),
                        Code = ErrorCodes.UserDoesNotExist.ToString("D")
                    };
                }
                await _authRepository.UpdateAccountUser(request.UserId, AccountStatus.ACTIVE);
                return new ActivateUserSuccessResponse
                {
                    Message = "User activated."
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ActivateUserErrorResponse
                {
                    Code = ErrorCodes.AnUnexpectedErrorOcurred.ToString("D"),
                    Message = Enum.GetName(ErrorCodes.AnUnexpectedErrorOcurred)
                };
            }
        }
    }
}
