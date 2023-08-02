
namespace Application.User.RefreshToken.Request
{
    public class RefreshTokenRequest : User.Request
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
