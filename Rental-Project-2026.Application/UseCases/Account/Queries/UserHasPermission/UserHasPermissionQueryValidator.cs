using FluentValidation;

namespace Rental_Project_2026.Application.UseCases.Account.Queries.UserHasPermission
{
    public class UserHasPermissionQueryValidator : AbstractValidator<UserHasPermissionQuery>
    {
        public UserHasPermissionQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("El ID del usuario es requerido.")
                .NotNull().WithMessage("El ID del usuario no puede estar nulo.");


            RuleFor(x => x.PermissionCode)
                .NotEmpty().WithMessage("El código de permiso es requerido.")
                .NotNull().WithMessage("El código de permiso no puede estar nulo.");
        }
    }
}
