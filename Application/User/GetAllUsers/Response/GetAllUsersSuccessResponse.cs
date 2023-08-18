

using Application.DTO;

namespace Application.User.GetAllUsers.Response
{
    public class GetAllUsersSuccessResponse : GetAllUsersResponse
    {
        public IEnumerable<UserDTO> UsersDTO{ get; set; }
    }
}
