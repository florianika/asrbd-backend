
namespace Application.User.CreateUser.Response
{
    public class CreateUserSuccessResponse : CreateUserResponse
    {
        public Guid UserId { get; internal set; }
    }
}
