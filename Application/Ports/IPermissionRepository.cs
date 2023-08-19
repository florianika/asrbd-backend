

using Domain.Enum;
using Domain.RolePermission;

namespace Application.Ports
{
    public interface IPermissionRepository
    {
        Task<long> CreateRolePermission(Domain.RolePermission.RolePermission rolePermission);

        Task<List<Domain.RolePermission.RolePermission>> GetAllPermissions();
        Task<List<Domain.RolePermission.RolePermission>> GetPermissionsByRole(AccountRole accountRole);
        Task<List<Domain.RolePermission.RolePermission>> GetPermissionsByRoleAndEntity(AccountRole accountRole, EntityType entityType);
        Task<List<Domain.RolePermission.RolePermission>> GetPermissionsByRoleAndEntityAndVariable(AccountRole accountRole, EntityType entityType, string variableName);
        Task<Domain.RolePermission.RolePermission> GetPermissionRoleById(long id);
        Task DeleteRolePermission(Domain.RolePermission.RolePermission rolePermission);
        Task UpdateRolePermission(long Id, Domain.RolePermission.RolePermission newRolePermission);


    }
}
