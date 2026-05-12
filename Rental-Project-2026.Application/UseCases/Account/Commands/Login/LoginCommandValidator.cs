using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Account.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("El usuario es requerido.");


            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es requerida.");
                
        }
    }
}
