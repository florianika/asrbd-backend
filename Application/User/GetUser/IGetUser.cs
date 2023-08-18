
using Application.User.GetUser.Request;
using Application.User.GetUser.Response;

namespace Application.User.GetUser
{
    public interface IGetUser : IUser<GetUserRequest, GetUserResponse>
    {
    }
}
