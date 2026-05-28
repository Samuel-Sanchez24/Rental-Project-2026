using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Reservations.Commands.CancelledReservation
{
    public class CancelledReservationValidator : AbstractValidator<CancelledReservationCommand>
    {
        public CancelledReservationValidator()
        {
            RuleFor(r => r.Id)
                .NotEmpty().WithMessage("El ID de la reserva es requerido.");
        }
    }
}
