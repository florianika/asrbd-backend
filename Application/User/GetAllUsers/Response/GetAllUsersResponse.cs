
namespace Application.User.GetAllUsers.Response
{
    public abstract class GetAllUsersResponse : User.Response
    {
        public IEnumerable<UserDTO> UsersTDO { get; set; }
    }
}
