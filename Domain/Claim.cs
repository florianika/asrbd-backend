namespace Domain
{
    #nullable disable
    /// <summary>
    /// Represents a claim associated with a user in the system.
    /// </summary>
    public class Claim
    {
        /// <summary>
        /// Gets or sets the unique identifier for the claim.
        /// </summary>
        public int ClaimId { get; set; }

        /// <summary>
        /// Gets or sets the type of the claim.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the value associated with the claim.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the user this claim belongs to.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the associated user object.
        /// </summary>
        public User User { get; set; }
    }
}