namespace Domain.Enum
{
    /// <summary>
    /// Represents permission levels for access control.
    /// </summary>
    public enum Permission
    {
        /// <summary>
        /// No permissions granted.
        /// </summary>
        NONE,

        /// <summary>
        /// Read-only permission granted.
        /// </summary>
        READ,

        /// <summary>
        /// Write permission granted, which typically includes read access as well.
        /// </summary>
        WRITE
    }
}