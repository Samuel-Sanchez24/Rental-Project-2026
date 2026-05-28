using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Reservations.Commands.CreateReservation
{
    public class CreateReservationValidator : AbstractValidator<CreateReservationCommand>
    {
        public CreateReservationValidator()
        {
            RuleFor(r => r.VehicleId)
                .NotEmpty().WithMessage("El vehículo es requerido.");

            RuleFor(r => r.UserId)
                .NotEmpty().WithMessage("El usuario es requerido.");

            RuleFor(r => r.RentDate)
                .NotEmpty().WithMessage("La fecha de renta es requerida.")
                .GreaterThanOrEqualTo(DateTime.Now.Date)
                .WithMessage("La fecha de renta no puede ser menor a la fecha actual.");

            RuleFor(r => r.ReturnDate)
                .NotEmpty().WithMessage("La fecha de devolución es requerida.")
                .GreaterThan(r => r.RentDate)
                .WithMessage("La fecha de devolución debe ser mayor a la fecha de renta.");
        }
    }
}
