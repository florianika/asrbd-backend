
using Application.DTO;
using Application.Exceptions;
using Application.Ports;
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
                var user = await _authRepository.FindUserById(request.UserId) ?? throw new NotFoundException("User not found");
                
                var userDTO = MapUserToDTO(user);

                return new GetUserSuccessResponse
                {
                    UserDTO = userDTO
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        
        private UserDTO MapUserToDTO(Domain.User.User user)
        {
            var userDTO = new UserDTO
            {
                Id = user.Id,
                AccountRole = user.AccountRole.ToString(),
                AccountStatus = user.AccountStatus.ToString(),
                Email = user.Email,
                Name = user.Name,
                LastName = user.LastName
            };

            return userDTO;
        }
    }
}
