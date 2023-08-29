

using Application.Enums;
using Application.Exceptions;
using Application.Ports;
using Application.RolePermission.CreateRolePermission.Response;
using Application.RolePermission.Request;
using Application.User.CreateUser.Response;
using Application.User.GetUser;
using Application.User.UpdateUserRole.Response;
using Domain.Enum;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace Application.RolePermission.CreateRolePermission
{
    public class CreateRolePermission : ICreateRolePermission
    {
        private readonly ILogger _logger;
        private readonly IPermissionRepository _permissionRepository;
        public CreateRolePermission(ILogger<GetUser> logger,IPermissionRepository permissionRepository)
        {
            _logger = logger;
            _permissionRepository = permissionRepository;
        }
        public async Task<CreateRolePermissionResponse> Execute(CreateRolePermissionRequest request)
        {
            //FIXME converting string to enum should be a configuration in the WebAPI
            //https://github.com/parstat/structural-metadata/blob/dev/Parstat.StructuralMetadata/Presentation/Presentation.WebApi/Startup.cs
            //https://jasonwatmore.com/post/2021/10/12/net-return-enum-as-string-from-api#:~:text=The%20solution%20is%20to%20add%20a%20JsonStringEnumConverter%20%28%29,responses%20%28e.g.%20Role%29%20x.JsonSerializerOptions.Converters.Add%20%28new%20JsonStringEnumConverter%20%28%29%29%3B%20%7D%29%3B
            try
            {

                var validationErrors = new List<string>();
                if (!Enum.IsDefined(typeof(AccountRole),request.Role))
                {
                    validationErrors.Add("Invalid role value.");
                }
                if (!Enum.IsDefined(typeof(EntityType), request.EntityType))
                {
                    validationErrors.Add("Invalid entity type value.");
                }

                if (!Enum.IsDefined(typeof(Permission), request.Permission))
                {
                    validationErrors.Add("Invalid permission value.");
                }

                if (validationErrors.Count > 0)
                {
                    throw new EnumExeption(validationErrors);
                }
                var rolePermission = new Domain.RolePermission.RolePermission
                {
                    Role = request.Role,
                    EntityType = request.EntityType,
                    VariableName = request.VariableName,
                    Permission = request.Permission
                };
                var result = await _permissionRepository.CreateRolePermission(rolePermission);
                if (result == null)
                {
                    throw new AppException("Something went wrong");
                }
                else
                {
                    return new CreateRolePermissionSuccessResponse
                    {
                        Id = result
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                throw;
            }
        }


    }
}
