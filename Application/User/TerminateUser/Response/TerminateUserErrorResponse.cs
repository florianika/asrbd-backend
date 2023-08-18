
namespace Application.User.TerminateUser.Response
{
    public class TerminateUserErrorResponse : TerminateUserResponse
    {
        public string Message { get; internal set; }
        public string Code { get; internal set; }
    }
}
