using Domain.Enum;
using System.Runtime.CompilerServices;

namespace Application.RolePermission
{
    public class RolePermissionDTO
    {
        public long Id { get; set; }
        public AccountRole Role { get; set; }
        public EntityType EntityType { get; set; }
        public string? VariableName { get; set; }
        public Permission Permission { get; set; }
    }
}
