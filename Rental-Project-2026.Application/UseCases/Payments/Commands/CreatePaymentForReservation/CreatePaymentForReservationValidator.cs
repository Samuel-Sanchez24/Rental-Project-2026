using FluentValidation;

namespace Rental_Project_2026.Application.UseCases.Payments.Commands.CreatePaymentForReservation
{
    public class CreatePaymentForReservationValidator : AbstractValidator<CreatePaymentForReservationCommand>
    {
        public CreatePaymentForReservationValidator()
        {
            RuleFor(p => p.ReservationId)
                .NotEmpty().WithMessage("La reserva es requerida para iniciar el pago.");

            RuleFor(p => p.UserId)
                .NotEmpty().WithMessage("El usuario autenticado es requerido.");
        }
    }
}
