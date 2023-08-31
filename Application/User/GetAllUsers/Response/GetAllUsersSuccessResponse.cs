namespace Application.User.GetAllUsers.Response
{
    public class GetAllUsersSuccessResponse : GetAllUsersResponse
    {
        public IEnumerable<UserDTO> UsersDTO{ get; set; } = new List<UserDTO>();
    }
}
