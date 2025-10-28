
using Application.User.ResetPassword.Request;
using Application.User.ResetPassword.Response;

namespace Application.User.ResetPassword
{
    public interface IResetPassword : IUser<ResetPasswordRequest, ResetPasswordResponse>
    {
    }
}
