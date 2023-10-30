﻿using Application.Exceptions;
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
                var user = await _authRepository.GetUserByUserId(request.UserId) ?? throw new NotFoundException("User not found");
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
                return new SignOutErrorResponse
                {
                    Code = "404",
                    Message = "User not found"
                };
            }
        }
    }
}
