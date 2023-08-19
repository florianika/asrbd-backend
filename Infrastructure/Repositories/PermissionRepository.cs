

using Application.Ports;
using Domain.Enum;
using Domain.RolePermission;
using Domain.User;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly DataContext _context;
        public PermissionRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<long> CreateRolePermission(RolePermission rolePermission)
        {
            await _context.RolePermissions.AddAsync(rolePermission);
            await _context.SaveChangesAsync();
            return rolePermission.Id;
        }

        public async Task<List<RolePermission>> GetAllPermissions()
        {
            return await _context.RolePermissions.ToListAsync();
        }

        public async Task<List<RolePermission>> GetPermissionsByRole(AccountRole accountRole)
        {
            return await _context.RolePermissions.Where(x=>x.Role==accountRole).ToListAsync();
        }

        public async Task<List<RolePermission>> GetPermissionsByRoleAndEntity(AccountRole accountRole, EntityType entityType)
        {
            return await _context.RolePermissions.Where(x => x.Role == accountRole 
            && x.EntityType==entityType).ToListAsync();
        }

        public async Task<List<RolePermission>> GetPermissionsByRoleAndEntityAndVariable(AccountRole accountRole, EntityType entityType, string variableName)
        {
            return await _context.RolePermissions.Where(x => x.Role == accountRole 
            && x.EntityType == entityType
            && x.VariableName==variableName).ToListAsync();
        }
        public async Task<RolePermission> GetPermissionRoleById(long id)
        {
            return await _context.RolePermissions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task DeleteRolePermission(Domain.RolePermission.RolePermission rolePermission)
        {
             _context.RolePermissions.Remove(rolePermission);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRolePermission(long Id, Domain.RolePermission.RolePermission newRolePermission)
        {
            var existingRolePermission = await _context.RolePermissions.FindAsync(Id);

            existingRolePermission.Role = newRolePermission.Role;
            existingRolePermission.Permission = newRolePermission.Permission;
            existingRolePermission.VariableName = newRolePermission.VariableName;
            existingRolePermission.EntityType = newRolePermission.EntityType;
            await _context.SaveChangesAsync();
        }
    }
}
