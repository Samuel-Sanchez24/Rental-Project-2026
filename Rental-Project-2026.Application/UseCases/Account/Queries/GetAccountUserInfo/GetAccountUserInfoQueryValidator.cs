using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Rental_Project_2026.Application.UseCases.Account.Queries.GetAccountUserInfo
{
    public class GetAccountUserInfoQueryValidator : AbstractValidator<GetAccountUserInfoQuery>
    {
        public GetAccountUserInfoQueryValidator()
        {
            RuleFor( u => u.UserId)
                .NotEmpty().WithMessage("El ID del usuario es requerido.")
                .NotNull().WithMessage("El ID del usuario no puede estar nulo.");
        }
    }
}
