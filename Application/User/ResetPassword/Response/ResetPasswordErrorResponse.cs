namespace Application.User.ResetPassword.Response
{
    public class ResetPasswordErrorResponse : ResetPasswordResponse
    {
        public string? Message { get; set; }
        public string? Code { get; set; }
    }
}
