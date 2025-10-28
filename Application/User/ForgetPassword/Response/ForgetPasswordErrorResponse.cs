
namespace Application.User.ForgetPassword.Response
{
    public class ForgetPasswordErrorResponse : ForgetPasswordResponse
    {
        public string? Message { get; set; }
        public string? Code { get; set; }
    }
}
