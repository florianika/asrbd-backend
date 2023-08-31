
using Application.RolePermission;
using Application.User;

namespace Application.Common.Translators
{
    public static class Translator
    {
        public static List<UserDTO> ToDTOList(List<Domain.User.User> users)
        {
            var usersDTO = new List<UserDTO>();
            users.ForEach(user => {
                usersDTO.Add(ToDTO(user));
            });
            return usersDTO;
        }
        
        public static UserDTO ToDTO(Domain.User.User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                AccountRole = user.AccountRole,
                AccountStatus = user.AccountStatus,
                Email = user.Email,
                Name = user.Name,
                LastName = user.LastName
            };
        }

        public static List<RolePermissionDTO> ToDTOList(List<Domain.RolePermission.RolePermission> rolePermissions) {
            List<RolePermissionDTO> permissions = new();
            rolePermissions.ForEach((rolePermission) => {
                permissions.Add(ToDTO(rolePermission));
            });
            return permissions;
        }
        
        public static RolePermissionDTO ToDTO(Domain.RolePermission.RolePermission rolePermission) {
            return new RolePermissionDTO() {
                Id = rolePermission.Id,
                EntityType = rolePermission.EntityType,
                Permission = rolePermission.Permission,
                Role = rolePermission.Role,
                VariableName = rolePermission.VariableName
            };
        }
    }
}
