using Domain.Enum;

namespace Application.User
{
    #nullable disable
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public AccountStatus AccountStatus { get; set; }
        public AccountRole AccountRole { get; set; }
    }
}
