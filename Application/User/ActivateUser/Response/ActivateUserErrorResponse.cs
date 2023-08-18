
namespace Application.User.ActivateUser.Response
{
    public class ActivateUserErrorResponse : ActivateUserResponse
    {
        public string Message { get; internal set; }
        public string Code { get; internal set; }
    }
}
