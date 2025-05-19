﻿using Application.Exceptions;
using Application.Ports;
using Application.User.UpdateUserRole.Request;
using Application.User.UpdateUserRole.Response;
using Microsoft.Extensions.Logging;

namespace Application.User.UpdateUserRole
{
    public class UpdateUserRole : IUpdateUserRole
    {
        private readonly ILogger _logger;
        private readonly IAuthRepository _authRepository;
        public UpdateUserRole(ILogger<UpdateUserRole> logger,
            IAuthRepository authRepository)
        {
            _logger = logger;
            _authRepository = authRepository;
        }
        public async Task<UpdateUserRoleResponse> Execute(UpdateUserRoleRequest request)
        {
            try
            {
                await _authRepository.UpdateUserRole(request.UserId, request.AccountRole);
                return new UpdateUserRoleSuccessResponse
                {
                    Message = "User role updated."
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
