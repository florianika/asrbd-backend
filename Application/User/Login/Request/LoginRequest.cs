namespace Application.User.Login.Request
{
    public class LoginRequest : User.Request
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
