
using Domain.Enum;

namespace Domain.RolePermission
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
