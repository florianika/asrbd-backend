

using Application.User.SignOut.Request;
using Application.User.SignOut.Response;

namespace Application.User.SignOut
{
    public interface ISignOut : IUser<SignOutRequest, SignOutResponse>
    {
    }
}
