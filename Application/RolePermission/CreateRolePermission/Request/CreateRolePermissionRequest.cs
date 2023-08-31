using Domain.Enum;
using FluentValidation;

namespace Application.RolePermission.Request
{
    public class CreateRolePermissionRequest : RolePermission.RequestRolePermission
    {
        //[ValidEnumValue(typeof(AccountRole))]
        public AccountRole Role { get; set; }
        //[ValidEnumValue(typeof(EntityType))]
        public EntityType EntityType { get; set; }
        public string? VariableName { get; set; }
        //[ValidEnumValue(typeof(Permission))]
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
