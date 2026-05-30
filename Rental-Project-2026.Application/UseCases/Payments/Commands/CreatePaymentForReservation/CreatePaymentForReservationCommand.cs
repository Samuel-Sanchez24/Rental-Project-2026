namespace Rental_Project_2026.Application.UseCases.Payments.Commands.CreatePaymentForReservation
{
    public class CreatePaymentForReservationCommand : IRequest<CreatePaymentForReservationResult>
    {
        public Guid ReservationId { get; set; }
        public string UserId { get; set; } = string.Empty;
    }
}
