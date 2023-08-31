
namespace Application.User.ActivateUser.Response
{
    public class ActivateUserErrorResponse : ActivateUserResponse
    {
        public string? Message { get;  set; }
        public string? Code { get; set; }
    }
}
