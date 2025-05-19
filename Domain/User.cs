#nullable disable
using Domain.Enum;

namespace Domain
{
    /// <summary>
    /// Represents a user entity in the system.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user account is active.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the hashed password of the user.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the salt used for password hashing.
        /// </summary>
        public string Salt { get; set; }

        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the last name of the user.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the collection of claims associated with the user.
        /// </summary>
        public IList<Claim> Claims { get; set; }

        /// <summary>
        /// Gets or sets the refresh token associated with the user.
        /// </summary>
        public RefreshToken RefreshToken { get; set; }

        /// <summary>
        /// Gets or sets the date when the user account was created.
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the date when the user account was last updated.
        /// </summary>
        public DateTime UpdateDate { get; set; }

        /// <summary>
        /// Gets or sets the current status of the user account.
        /// </summary>
        public AccountStatus AccountStatus { get; set; }

        /// <summary>
        /// Gets or sets the role assigned to the user account.
        /// </summary>
        public AccountRole AccountRole { get; set; }

        /// <summary>
        /// Gets or sets the number of failed sign-in attempts.
        /// </summary>
        public int SigninFails { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the account lock expires.
        /// </summary>
        public DateTime? LockExpiration { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        public User()
        {
            Claims = new List<Claim>();
            RefreshToken = new RefreshToken();
        }
    }
}