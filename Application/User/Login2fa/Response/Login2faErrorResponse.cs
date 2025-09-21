namespace Application.User.Login2fa.Response
{
    public class Login2faErrorResponse : Login2faResponse
    {
        public string? Message { get; internal set; }
        public string? Code { get; internal set; }
    }
}
