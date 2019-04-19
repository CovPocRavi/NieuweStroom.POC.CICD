using FluentValidation;

namespace NieuweStroom.POC.IT.Controllers.Roles
{
    public class RoleValidator : AbstractValidator<SaveRoleResource>
    {
        public RoleValidator()
        {
            RuleFor(c => c.Description).NotEmpty().MinimumLength(4).MaximumLength(32);
        }
    }
}