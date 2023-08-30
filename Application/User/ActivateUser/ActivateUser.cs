﻿using Application.Enums;
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
                await ActivateUserAsync(request.UserId.ToString());
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
        private async Task ActivateUserAsync(string userId)
        {
            var userExists = await _authRepository.CheckIfUserExists(new Guid(userId));
            if (!userExists)
            {
                throw new NotFoundException("User not found");
            }
            await _authRepository.UpdateAccountUser(new Guid(userId), AccountStatus.ACTIVE);
        }
    }
}
