using Application.User.Verify2fa.Request;
using Application.User.Verify2fa.Response;

namespace Application.User.Verify2fa
{
    public interface IVerify2fa : IUser<Verify2faRequest, Verify2faResponse>
    {
    }
}
