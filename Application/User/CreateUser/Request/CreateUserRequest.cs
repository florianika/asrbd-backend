using Domain.Claim;

namespace Application.User.CreateUser.Request
{
    public class CreateUserRequest : User.Request
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public IList<Claim> Claims { get; set; }
    }
}
