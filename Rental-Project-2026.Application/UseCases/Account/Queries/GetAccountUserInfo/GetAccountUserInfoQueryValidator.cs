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
                .NotEmpty().WithMessage("UserId is required.")
                .NotNull().WithMessage("UserId cannot be null.");
        }
    }
}
