using Domain.Enum;

#nullable disable
namespace Domain
{
    /// <summary>
    /// Represents a permission associated with a specific role for entity access control.
    /// </summary>
    public class RolePermission
    {
        /// <summary>
        /// Gets or sets the unique identifier for the role permission.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the account role to which this permission applies.
        /// </summary>
        public AccountRole Role { get; set; }

        /// <summary>
        /// Gets or sets the type of entity this permission is for.
        /// </summary>
        public EntityType EntityType { get; set; }

        /// <summary>
        /// Gets or sets the name of the variable or field this permission applies to.
        /// </summary>
        public string VariableName { get; set; }

        /// <summary>
        /// Gets or sets the type of permission granted (e.g., Read, Write, Delete).
        /// </summary>
        public Permission Permission { get; set; }
    }
}