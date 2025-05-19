namespace Domain.Enum
{
    /// <summary>
    /// Represents the possible roles for an account in the system.
    /// </summary>
    public enum AccountRole
    {
        /// <summary>
        /// Administrator role with full system access.
        /// </summary>
        ADMIN,

        /// <summary>
        /// Supervisor role with oversight capabilities.
        /// </summary>
        SUPERVISOR,

        /// <summary>
        /// Enumerator role for data collection purposes.
        /// </summary>
        ENUMERATOR,

        /// <summary>
        /// Client role for basic system access.
        /// </summary>
        CLIENT,

        /// <summary>
        /// Publisher role with content publishing privileges.
        /// </summary>
        PUBLISHER,

        /// <summary>
        /// Standard user role with basic access rights.
        /// </summary>
        USER
    }
}