using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Vehicles.Commands.ChangeStatusVehicle
{
    public class ChangeStatusVehicleValidator : AbstractValidator<ChangeStatusVehicleCommand>
    {
        public ChangeStatusVehicleValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("El vehiculo es obligatorio.");
            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("El estado del vehiculo es inválido.");
        }
    }
}
