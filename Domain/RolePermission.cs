
using Domain.Enum;

#nullable disable
namespace Domain
{
    public class RolePermission
    {
        public long Id { get; set; }
        public AccountRole Role { get; set; }
        public EntityType EntityType { get; set; }
        public string VariableName { get; set; }
        public Permission Permission { get; set; }

    }
}
