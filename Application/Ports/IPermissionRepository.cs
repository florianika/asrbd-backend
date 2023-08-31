

using Application.RolePermission.UpdateRolePermission.Request;
using Domain.Enum;

namespace Application.Ports
{
    public interface IPermissionRepository
    {
        Task<long> CreateRolePermission(Domain.RolePermission rolePermission);

        Task<List<Domain.RolePermission>> GetAllPermissions();
        Task<List<Domain.RolePermission>> GetPermissionsByRole(AccountRole accountRole);
        Task<List<Domain.RolePermission>> GetPermissionsByRoleAndEntity(AccountRole accountRole, EntityType entityType);
        Task<List<Domain.RolePermission>> GetPermissionsByRoleAndEntityAndVariable(AccountRole accountRole, EntityType entityType, string variableName);
        Task<Domain.RolePermission> GetPermissionRoleById(long id);
        Task DeleteRolePermission(long id);
        Task UpdateRolePermission(UpdateRolePermissionRequest updateRolePermissionRequest);


    }
}
