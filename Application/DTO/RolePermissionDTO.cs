using System.Runtime.CompilerServices;

namespace Application.DTO
{
    public class RolePermissionDTO
    {
        public long Id { get; set; }
        //FIXME this should be enum
        public string Role { get; set; }
        //FIXME this should be enum
        public string EntityType { get; set; }
        public string VariableName { get; set;}
        //FIXME this should be enum
        public string Permission { get; set; }
    }
}
