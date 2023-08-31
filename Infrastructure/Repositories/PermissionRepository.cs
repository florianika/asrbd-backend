using Application.Ports;
using Domain.Enum;
using Domain;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Application.Exceptions;
using Application.RolePermission.UpdateRolePermission.Request;

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
            return await _context.RolePermissions.FirstOrDefaultAsync(x => x.Id == id)
                                ?? throw new NotFoundException("Permission not found");;
        }

        public async Task DeleteRolePermission(long id)
        {
            var rolePermission = await _context.RolePermissions.SingleOrDefaultAsync(rp => rp.Id == id);
            if(rolePermission != null)  {
                _context.RolePermissions.Remove(rolePermission);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateRolePermission(UpdateRolePermissionRequest updateRolePermissionRequest)
        {
            var existingRolePermission = await _context.RolePermissions.FindAsync(updateRolePermissionRequest.Id)
                ?? throw new NotFoundException("Permission not found");;

            existingRolePermission.Role = updateRolePermissionRequest.Role;
            existingRolePermission.Permission = updateRolePermissionRequest.Permission;
            existingRolePermission.VariableName = updateRolePermissionRequest.VariableName;
            existingRolePermission.EntityType = updateRolePermissionRequest.EntityType;
            await _context.SaveChangesAsync();
        }

    }
}
