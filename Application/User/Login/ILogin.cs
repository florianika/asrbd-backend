
using Application.User.CreateUser.Request;
using Application.User.CreateUser.Response;
using Application.User.Login.Request;
using Application.User.Login.Response;

namespace Application.User.Login
{
    public interface ILogin : IUser<LoginRequest, LoginResponse>
    {
    }
}
