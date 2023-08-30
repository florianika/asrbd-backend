using Application.Enums;
using Application.Exceptions;
using Application.Ports;
using Application.User.SignOut.Response;
using Application.User.UpdateUserRole.Request;
using Application.User.UpdateUserRole.Response;
using Domain.Enum;
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
        //FIXME refactor method and sparate error handling
        public async Task<UpdateUserRoleResponse> Execute(UpdateUserRoleRequest request)
        {
            /*
            try
            {
                var userExists = await _authRepository.CheckIfUserExists(request.UserId);
                if (userExists == false)
                {
                    throw new DirectoryNotFoundException("User not found");
                }
                var validationErrors = new List<string>();
                if (!Enum.IsDefined(typeof(AccountRole), request.AccountRole))
                {
                    validationErrors.Add("Invalid role value.");
                }
                if (validationErrors.Count > 0)
                {
                    throw new EnumExeption(validationErrors);
                }
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
        */
            try
            {
                await ValidateAndUpdateUserRoleAsync(request);
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
        private async Task ValidateAndUpdateUserRoleAsync(UpdateUserRoleRequest request)
        {
            var userExists = await _authRepository.CheckIfUserExists(request.UserId);
            if (!userExists)
            {
                throw new DirectoryNotFoundException("User not found");
            }

            var validationErrors = new List<string>();
            if (!Enum.IsDefined(typeof(AccountRole), request.AccountRole))
            {
                validationErrors.Add("Invalid role value.");
            }
            if (validationErrors.Count > 0)
            {
                throw new Application.Exceptions.EnumExeption(validationErrors);
            }

            await _authRepository.UpdateUserRole(request.UserId, request.AccountRole);
        }
    }
}
