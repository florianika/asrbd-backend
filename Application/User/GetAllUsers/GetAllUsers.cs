

using Application.Common.Translators;
using Application.Enums;
using Application.Ports;
using Application.User.CreateUser.Request;
using Application.User.CreateUser.Response;
using Application.User.GetAllUsers.Response;
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
        //FIXME refactor this, suggestion add a translator statisc class that has methods to translate from enum to DTO
        //Separate exception handler
        public async Task<GetAllUsersResponse> Execute()
        {
            try
            {
                var users = await _authRepository.GetAllUsers();
                var usersDTO = UserTranslator.TranslateToDTOList(users);

                return new GetAllUsersSuccessResponse
                {
                    UsersDTO = usersDTO
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
