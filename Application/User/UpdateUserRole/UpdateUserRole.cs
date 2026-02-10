using Application.Exceptions;
using Application.Ports;
using Application.User.SetUserMunicipality.Request;
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
        public async Task<UpdateUserRoleResponse> Execute(UpdateUserRoleRequest request)
        {
            ValidateUserUpdateRole(request);
            try
            {
                if (request.RequestUserRole != null)
                    await _authRepository.UpdateUserRole(request.UserId, request.AccountRole, request.RequestUserRole);
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
        private void ValidateUserUpdateRole(UpdateUserRoleRequest request)
        {
            try
            {
                if (request.UserId == request.RequestUserId)
                    throw new ForbidenException("Cannot update role for yourself.");

                Enum.TryParse(request.RequestUserRole, out AccountRole accountRole);
                if (accountRole is not (AccountRole.ADMIN or AccountRole.SUPERVISOR))
                    throw new ForbidenException("Cannot update role for user.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
