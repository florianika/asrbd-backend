namespace Domain
{
    #nullable disable
    /// <summary>
    /// Represents a refresh token entity used for authentication and authorization.
    /// </summary>
    public class RefreshToken
    {
        /// <summary>
        /// Gets or sets the unique identifier for the refresh token.
        /// </summary>
        public int RefreshTokenId { get; set; }

        /// <summary>
        /// Gets or sets the token value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the refresh token is active.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the expiration date and time of the refresh token.
        /// </summary>
        public DateTime ExpirationDate { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the user this token belongs to.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the associated user for this refresh token.
        /// </summary>
        public User User { get; set; }
    }
}