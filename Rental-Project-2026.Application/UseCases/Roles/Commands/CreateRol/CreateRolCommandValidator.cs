using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Roles.Commands.CreateRol
{
    public sealed class CreateRolCommandValidator : AbstractValidator<CreateRolCommand>
    {
        public CreateRolCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre del rol es obligatorio.")
                .MaximumLength(64).WithMessage("El nombre del rol no debe exceder 64 caracteres.")
                .MinimumLength(3).WithMessage("El nombre del rol debe tener al menos 3 caracteres.");
        }
    }
}
