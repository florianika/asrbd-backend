
using Application.User.ForgetPassword.Request;
using Application.User.ForgetPassword.Response;

namespace Application.User.ForgetPassword
{
    public interface IForgetPassword : IUser<ForgetPasswordRequest, ForgetPasswordResponse>
    {
    }
}
