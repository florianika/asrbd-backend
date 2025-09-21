namespace Application.User.Login2fa.Response
{
    public class Login2faSuccessResponse : Login2faResponse
    {
        public Guid UserId { get; set; }
        public string Message { get; set; } = "2FA required";
    }
}
