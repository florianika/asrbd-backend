using Domain.Enum;
using System.Runtime.CompilerServices;

namespace Application.RolePermission
{
    public class RolePermissionDTO
    {
        public long Id { get; set; }
        //FIXME this should be enum
        public AccountRole Role { get; set; }
        //FIXME this should be enum
        public EntityType EntityType { get; set; }
        public string VariableName { get; set; }
        //FIXME this should be enum
        public Permission Permission { get; set; }
    }
}
