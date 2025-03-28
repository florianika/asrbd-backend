using Application.User.GetUserByEmail.Request;
using Application.User.GetUserByEmail.Response;

namespace Application.User.GetUserByEmail;

public interface IGetUserByEmail : IUser<GetUserByEmailRequest, GetUserByEmailResponse>
{
    
}