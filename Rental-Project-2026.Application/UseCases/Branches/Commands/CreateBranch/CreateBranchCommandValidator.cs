using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Branches.Commands.CreateBranch
{
    public class CreateBranchCommandValidator : AbstractValidator<CreateBranchCommand>
    {
        public CreateBranchCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre de la sucursal es requerido.")
                .MaximumLength(100).WithMessage("El nombre de la sucursal no puede exceder los 100 caracteres.")
                .MinimumLength(4).WithMessage("El nombre de la sucursal debe tener al menos 4 caracteres");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("La ciudad de la sucursal es requerido.")
                .MaximumLength(20).WithMessage("La ciudad de la sucursal no puede exceder los 20 caracteres.")
                .MinimumLength(4).WithMessage("La ciudad de la sucursal debe tener al menos 4 caracteres");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("La direccion de la sucursal es requerida.")
                .MaximumLength(200).WithMessage("La direccion de la sucursal no puede exceder los 200 caracteres.")
                .MinimumLength(5).WithMessage("La direccion de la sucursal debe tener al menos 5 caracteres");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("El numero de telefono de la sucursal es requerido.")
                .MaximumLength(20).WithMessage("El numero de telefono de la sucursal no puede exceder los 20 caracteres.")
                .MinimumLength(7).WithMessage("El numero de telefono de la sucursal debe tener al menos 7 caracteres");
        }
    }
}
    