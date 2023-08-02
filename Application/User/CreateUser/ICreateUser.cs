using Application.User.CreateUser.Request;
using Application.User.CreateUser.Response;


namespace Application.User.CreateUser
{
    public interface ICreateUser: IUser<CreateUserRequest, CreateUserResponse>
    {
    }
}
