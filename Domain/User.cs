#nullable disable
using Domain.Enum;

namespace Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public bool Active { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public IList<Claim> Claims { get; set; }
        public RefreshToken RefreshToken { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public AccountStatus AccountStatus { get; set; }
        public AccountRole AccountRole { get; set; }
        public int SigninFails { get; set; }
        public DateTime? LockExpiration { get; set; }
        public User()
        {
            Claims = new List<Claim>();
            RefreshToken = new RefreshToken();
        }
    }
}
