
namespace Application.User.GetAllUsers.Response
{
    public class GetAllUsersErrorResponse : GetAllUsersResponse
    {
        public string? Message { get; set; }
        public string? Code { get; set; }
    }
}
