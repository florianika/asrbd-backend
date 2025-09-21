using Application.User.Login2fa.Request;
using Application.User.Login2fa.Response;

namespace Application.User.Login2fa
{
    public interface ILogin2fa : IUser<Login2faRequest, Login2faResponse>
    {
    }
}
