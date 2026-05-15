using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Rental_Project_2026.Application.UseCases.Account.Queries.UserHasPermission
{
    public class UserHasPermissionQueryValidator : AbstractValidator<UserHasPermissionQuery>
    {
        public UserHasPermissionQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.")
                .NotNull().WithMessage("UserId cannot be null.");


            RuleFor(x => x.PermissionCode)
                .NotEmpty().WithMessage("PermissionCode is required.")
                .NotNull().WithMessage("PermissionCode cannot be null.");
        }
    }
}
