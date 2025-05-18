using Application.Enums;
using Application.Exceptions;
using Application.Ports;
using Application.User.ActivateUser.Request;
using Application.User.ActivateUser.Response;
using Application.User.TerminateUser.Response;
using Domain.Enum;
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
        //FIXME sparate error handling here and refactor the method.
        public async Task<ActivateUserResponse> Execute(ActivateUserRequest request)
        {
            try
            {
                //TODO this should be removed existence of the user is checked by UpdateUserAccount
                /*var exists = await _authRepository.CheckIfUserExists(request.UserId);
                if (!exists)
                {
                    throw new NotFoundException("User not found");
                }*/
                await _authRepository.UpdateAccountUser(request.UserId, AccountStatus.ACTIVE);
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
