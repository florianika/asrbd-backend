
namespace Application.User.GetUser.Response
{
    public class GetUserErrorResponse : GetUserResponse
    {
        public string? Message { get; set; }
        public string? Code { get; set; }
    }
}
