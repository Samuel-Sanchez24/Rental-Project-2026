using Rental_Project_2026.Application.Contracts.Payments;
using Rental_Project_2026.Application.Contracts.Repositories;
using Rental_Project_2026.Domain.Entities;
using Rental_Project_2026.Domain.Enums;
using Rental_Project_2026.Domain.Exceptions;

namespace Rental_Project_2026.Application.UseCases.Payments.Commands.CreatePaymentForReservation
{
    public class CreatePaymentForReservationUseCase : IRequestHandler<CreatePaymentForReservationCommand, CreatePaymentForReservationResult>
    {
        private readonly IReservationsRepository _reservationsRepository;
        private readonly IPaymentsRepository _paymentsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentGateway _paymentGateway;

        public CreatePaymentForReservationUseCase(
            IReservationsRepository reservationsRepository,
            IPaymentsRepository paymentsRepository,
            IUnitOfWork unitOfWork,
            IPaymentGateway paymentGateway)
        {
            _reservationsRepository = reservationsRepository;
            _paymentsRepository = paymentsRepository;
            _unitOfWork = unitOfWork;
            _paymentGateway = paymentGateway;
        }

        public async Task<CreatePaymentForReservationResult> Handle(CreatePaymentForReservationCommand command)
        {
            Reservation? reservation = await _reservationsRepository.GetReservationByIdAsync(command.ReservationId);

            if (reservation is null)
                throw new BusinessRulesException("La reserva no existe.");

            if (reservation.UserId != command.UserId)
                throw new BusinessRulesException("No tienes acceso para pagar esta reserva.");

            if (reservation.Status != ReservationStatus.Pending)
                throw new BusinessRulesException("Solo se pueden pagar reservas pendientes de pago.");

            Payment? successfulPayment = await _paymentsRepository.GetPaidByReservationIdAsync(reservation.Id);

            if (successfulPayment is not null)
                throw new BusinessRulesException("La reserva ya tiene un pago aprobado.");

            Payment? pendingPayment = await _paymentsRepository.GetPendingByReservationIdAsync(reservation.Id);

            if (pendingPayment is not null &&
                !string.IsNullOrWhiteSpace(pendingPayment.PaymentUrl))
            {
                return new CreatePaymentForReservationResult
                {
                    PaymentId = pendingPayment.Id,
                    ProviderReference = pendingPayment.ProviderReference ?? string.Empty,
                    PaymentUrl = pendingPayment.PaymentUrl!,
                    Status = pendingPayment.Status,
                    ReusedExistingPayment = true,
                    Message = "Se reutilizó la intención de pago pendiente."
                };
            }

            Payment payment = new Payment(
                reservation.Id,
                reservation.TotalPrice,
                _paymentGateway.ProviderName);

            PaymentGatewayCreateRequest gatewayRequest = new PaymentGatewayCreateRequest
            {
                PaymentId = payment.Id,
                ReservationId = reservation.Id,
                Amount = reservation.TotalPrice,
                CustomerEmail = reservation.Email,
                Description = $"Pago reserva {reservation.Id}",
                ReturnUrl = null,
                WebhookUrl = null
            };

            try
            {
                if (pendingPayment is not null)
                {
                    pendingPayment.MarkAsCancelled();
                    await _paymentsRepository.UpdateAsync(pendingPayment);
                }

                PaymentGatewayCreateResponse gatewayResponse =
                    await _paymentGateway.CreatePaymentAsync(gatewayRequest);

                payment.RegisterProviderResponse(
                    gatewayResponse.ProviderReference,
                    gatewayResponse.PaymentUrl,
                    gatewayResponse.Status);

                await _paymentsRepository.CreateAsync(payment);

                if (gatewayResponse.Status == PaymentStatus.Paid)
                    reservation.Confirm();

                await _unitOfWork.CommitAsync();

                return new CreatePaymentForReservationResult
                {
                    PaymentId = payment.Id,
                    ProviderReference = payment.ProviderReference ?? string.Empty,
                    PaymentUrl = payment.PaymentUrl ?? string.Empty,
                    Status = payment.Status,
                    ReusedExistingPayment = false,
                    Message = gatewayResponse.Message
                };
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
    }
}
