
using Application.DTO;
using Application.Enums;
using Application.Ports;
using Application.User.ActivateUser.Response;
using Application.User.GetAllUsers.Response;
using Application.User.GetUser.Request;
using Application.User.GetUser.Response;
using Microsoft.Extensions.Logging;

namespace Application.User.GetUser
{
    public class GetUser : IGetUser
    {
        private readonly ILogger _logger;
        private readonly IAuthRepository _authRepository;
        public GetUser(ILogger<GetUser> logger,
            IAuthRepository authRepository)
        {
            _logger = logger;
            _authRepository = authRepository;
        }
        //FIXME refactor this method and try to separate the error flow
        public async Task<GetUserResponse> Execute(GetUserRequest request)
        {
            try
            {
                var user = await _authRepository.FindUserById(request.UserId);

                if (user == null)
                {
                    return HandleUserNotFoundError();
                }

                var userDTO = MapUserToDTO(user);

                return new GetUserSuccessResponse
                {
                    UserDTO = userDTO
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return HandleUnexpectedError();
            }
        }
        private GetUserErrorResponse HandleUserNotFoundError()
        {
            return new GetUserErrorResponse
            {
                Message = Enum.GetName(ErrorCodes.UserDoesNotExist),
                Code = ErrorCodes.UserDoesNotExist.ToString("D")
            };
        }
        private GetUserErrorResponse HandleUnexpectedError()
        {
            return new GetUserErrorResponse
            {
                Message = Enum.GetName(ErrorCodes.AnUnexpectedErrorOcurred),
                Code = ErrorCodes.AnUnexpectedErrorOcurred.ToString("D")
            };
        }
        private UserDTO MapUserToDTO(Domain.User.User user)
        {
            var userDTO = new UserDTO();
            userDTO.Id = user.Id;
            userDTO.AccountRole = user.AccountRole.ToString();
            userDTO.AccountStatus = user.AccountStatus.ToString();
            userDTO.Email = user.Email;
            userDTO.Name = user.Name;
            userDTO.LastName = user.LastName;

            return userDTO;
        }
    }
}
