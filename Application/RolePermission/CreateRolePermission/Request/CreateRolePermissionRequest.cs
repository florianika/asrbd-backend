using Domain.Enum;
using FluentValidation;

namespace Application.RolePermission.Request
{
    public class CreateRolePermissionRequest : RolePermission.RequestRolePermission
    {
        public AccountRole Role { get; set; }
        public EntityType EntityType { get; set; }
        public string? VariableName { get; set; }
        public Permission Permission{ get; set; }

    }

    public class CreateRolePermissionRequestValidation : AbstractValidator<CreateRolePermissionRequest> 
    {
        public CreateRolePermissionRequestValidation() 
        {
            RuleFor(rp => rp.VariableName).NotEmpty().NotNull();
        }
    }
}
