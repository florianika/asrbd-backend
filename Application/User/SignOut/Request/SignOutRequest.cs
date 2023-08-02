namespace Application.User.SignOut.Request
{
    public class SignOutRequest : User.Request
    {
        public Guid UserId { get; set; }
    }
}
