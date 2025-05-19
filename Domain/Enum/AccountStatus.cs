namespace Domain.Enum
{
    /// <summary>
    /// Represents the current status of an account in the system.
    /// </summary>
    public enum AccountStatus
    {
        /// <summary>
        /// The account is active and fully operational.
        /// </summary>
        ACTIVE,

        /// <summary>
        /// The account is temporarily locked and cannot be accessed.
        /// </summary>
        LOCKED,

        /// <summary>
        /// The account has been permanently terminated and is no longer accessible.
        /// </summary>
        TERMINATED,

        /// <summary>
        /// The account is created but awaiting confirmation/verification.
        /// </summary>
        UNCONFIRMED,

        /// <summary>
        /// The account is managed and operated automatically by the system.
        /// </summary>
        AUTOMATIC
    }
}