namespace Application.User.Verify2fa.Response
{
    public class Verify2faSuccessResponse : Verify2faResponse
    {
        public string IdToken { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
