using Application.Common.Translators;
using Application.Ports;
using Application.User.GetAllUsers.Response;
using Domain.Enum;
using Microsoft.Extensions.Logging;

namespace Application.User.GetAllUsers
{
    public class GetAllUsers : IGetAllUsers
    {
        private readonly IAuthRepository _authRepository;
        private readonly ILogger _logger;
        public GetAllUsers(IAuthRepository authRepository, ILogger<GetAllUsers> logger)
        {
            _authRepository = authRepository;
            _logger = logger;
        }
        public async Task<GetAllUsersResponse> Execute(Guid requestUserId, string? role)
        {
            try
            {
                List<Domain.User> users;
                Enum.TryParse(role, true, out AccountRole accountRole);
                if (accountRole == AccountRole.ADMIN)
                {
                    users = await _authRepository.GetAllUsers(requestUserId);
                }
                else
                {
                    users = await _authRepository.GetAllNonAdminUsers(requestUserId);
                }
                var usersDto = Translator.ToDTOList(users);

                return new GetAllUsersSuccessResponse
                {
                    UsersDTO = usersDto
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
