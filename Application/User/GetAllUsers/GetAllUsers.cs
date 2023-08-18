﻿

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
        public async Task<GetAllUsersResponse> Execute()
        {
            try
            {
                var users = await _authRepository.GetAllUsers();
                var usersDTO = new List<UserDTO>();
                foreach (var user in users)
                {
                    var userDTO = new UserDTO();
                    userDTO.Id=user.Id;
                    userDTO.AccountRole = user.AccountRole;
                    userDTO.AccountStatus = user.AccountStatus;
                    userDTO.AccountRole = user.AccountRole;
                    userDTO.Email = user.Email;
                    userDTO.Name = user.Name;
                    userDTO.LastName= user.LastName;
                    usersDTO.Add(userDTO);
                }
                return new GetAllUsersSuccessResponse
                {
                    UsersDTO = usersDTO
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                var response = new GetAllUsersErrorResponse
                {
                    Message = Enum.GetName(ErrorCodes.AnUnexpectedErrorOcurred),
                    Code = ErrorCodes.AnUnexpectedErrorOcurred.ToString("D")
                };

                return response;
            }
        }
    }
}
