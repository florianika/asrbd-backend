
namespace Application.User.RefreshToken.Request
{
    #nullable disable
    public class RefreshTokenRequest : User.Request
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
