namespace Application.User.Verify2fa.Response
{
    public class Verify2faSuccessResponse : Verify2faResponse
    {
        public required string IdToken { get; set; }
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
    }
}
