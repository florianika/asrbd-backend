
using Application.User.RefreshToken.Request;
using Application.User.RefreshToken.Response;

namespace Application.User.RefreshToken
{
    public interface IRefreshToken : IUser<RefreshTokenRequest, RefreshTokenResponse>
    {
    }
}
