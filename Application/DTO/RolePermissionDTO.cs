namespace Application.DTO
{
    public class RolePermissionDTO
    {
        public long Id { get; set; }
        public string Role { get; set; }
        public string EntityType { get; set; }
        public string VariableName { get; set; }
        public string Permission { get; set; }
    }
}
