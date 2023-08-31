
using Domain.Enum;
using FluentValidation;

namespace Application.User.UpdateUserRole.Request
{
    public class UpdateUserRoleRequest : User.Request
    {
        public Guid UserId { get; set; }
        public AccountRole AccountRole { get; set; }
    }

    public class UpdateUserRoleRequestValidator : AbstractValidator<UpdateUserRoleRequest>
    {
        public UpdateUserRoleRequestValidator() 
        {
            RuleFor(uur => uur.AccountRole).NotEmpty().NotNull();
            RuleFor(uur => uur.UserId).NotEmpty().NotNull();
        }
    }
}
