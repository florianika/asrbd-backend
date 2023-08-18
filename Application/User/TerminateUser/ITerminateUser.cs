

using Application.User.TerminateUser.Request;
using Application.User.TerminateUser.Response;

namespace Application.User.TerminateUser
{
    public interface ITerminateUser : IUser<TerminateUserRequest, TerminateUserResponse>
    {
    }
}
