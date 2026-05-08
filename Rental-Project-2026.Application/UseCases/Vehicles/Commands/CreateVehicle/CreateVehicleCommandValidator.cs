using FluentValidation;
using Rental_Project_2026.Application.UseCases.Branches.Commands.CreateBranch;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Vehicles.Commands.CreateVehicle
{
    internal class CreateVehicleCommandValidator : AbstractValidator<CreateVehicleCommand>
    {
        public CreateVehicleCommandValidator()
        {
            RuleFor(v => v.Plate)
                .NotEmpty().WithMessage("La placa del vehículo es requerida.")
                .MaximumLength(20).WithMessage("La placa del vehículo no puede exceder los 20 caracteres.")
                .MinimumLength(4).WithMessage("La placa del vehículo debe tener al menos 4 caracteres");

            RuleFor(v => v.Model)
                .NotEmpty().WithMessage("El modelo del vehículo es requerido.")
                .MaximumLength(50).WithMessage("El modelo del vehículo no puede exceder los 50 caracteres.")
                .MinimumLength(2).WithMessage("El modelo del vehículo debe tener al menos 2 caracteres");

            RuleFor(v => v.Brand)
                .NotEmpty().WithMessage("La marca del vehículo es requerida.")
                .MaximumLength(50).WithMessage("La marca del vehículo no puede exceder los 50 caracteres.")
                .MinimumLength(2).WithMessage("La marca del vehículo debe tener al menos 2 caracteres");

            RuleFor(v => v.Color)
                .NotEmpty().WithMessage("El color del vehículo es requerido.")
                .MaximumLength(30).WithMessage("El color del vehículo no puede exceder los 30 caracteres.")
                .MinimumLength(3).WithMessage("El color del vehículo debe tener al menos 3 caracteres");

            RuleFor(v => v.Year)
                .GreaterThanOrEqualTo(1900).WithMessage("El año del vehículo debe ser mayor o igual a 1900.")
                .LessThanOrEqualTo(DateTime.Now.Year + 1).WithMessage($"El año del vehículo no puede ser mayor a {DateTime.Now.Year + 1}.");

            RuleFor(v => v.DailyPrice)
                .GreaterThan(0).WithMessage("El precio diario del vehículo debe ser mayor a cero.");

            RuleFor(v => v.BranchId)
                .NotEmpty().WithMessage("El ID de la sucursal es requerido.");
        }
    }
}
