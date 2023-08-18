
using Application.DTO;

namespace Application.User.GetUser.Response
{
    public class GetUserSuccessResponse : GetUserResponse
    {
        public UserDTO UserDTO { get; set; }
    }
}
