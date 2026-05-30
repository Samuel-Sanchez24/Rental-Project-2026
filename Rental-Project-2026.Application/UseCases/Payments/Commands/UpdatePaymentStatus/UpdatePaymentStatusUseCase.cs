using Rental_Project_2026.Application.Contracts.Payments;
using Rental_Project_2026.Application.Contracts.Repositories;
using Rental_Project_2026.Domain.Entities;
using Rental_Project_2026.Domain.Enums;
using Rental_Project_2026.Domain.Exceptions;

namespace Rental_Project_2026.Application.UseCases.Payments.Commands.UpdatePaymentStatus
{
    public class UpdatePaymentStatusUseCase : IRequestHandler<UpdatePaymentStatusCommand>
    {
        private readonly IPaymentsRepository _paymentsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentGateway _paymentGateway;

        public UpdatePaymentStatusUseCase(
            IPaymentsRepository paymentsRepository,
            IUnitOfWork unitOfWork,
            IPaymentGateway paymentGateway)
        {
            _paymentsRepository = paymentsRepository;
            _unitOfWork = unitOfWork;
            _paymentGateway = paymentGateway;
        }

        public async Task Handler(UpdatePaymentStatusCommand command)
        {
            Payment? payment = command.PaymentId.HasValue
                ? await _paymentsRepository.GetPaymentByIdAsync(command.PaymentId.Value)
                : await _paymentsRepository.GetByProviderReferenceAsync(command.ProviderReference ?? string.Empty);

            if (payment is null)
                throw new BusinessRulesException("El pago no existe.");

            if (!string.IsNullOrWhiteSpace(command.CurrentUserId) &&
                payment.Reservation.UserId != command.CurrentUserId)
            {
                throw new BusinessRulesException("No tienes acceso para actualizar este pago.");
            }

            if (command.ValidateProviderCallback)
            {
                bool isValid = await _paymentGateway.ValidateWebhookAsync(
                    new PaymentGatewayWebhookValidationRequest
                    {
                        ProviderReference = payment.ProviderReference ?? command.ProviderReference ?? string.Empty,
                        Status = command.Status,
                        Signature = command.Signature,
                        Payload = command.Payload
                    });

                if (!isValid)
                    throw new BusinessRulesException("La notificación del proveedor de pagos no es válida.");
            }

            if (payment.Status == PaymentStatus.Paid &&
                command.Status != PaymentStatus.Paid)
            {
                return;
            }

            switch (command.Status)
            {
                case PaymentStatus.Pending:
                    payment.MarkAsPending();
                    break;

                case PaymentStatus.Paid:
                    if (payment.Reservation.Status == ReservationStatus.Cancelled)
                        throw new BusinessRulesException("No se puede aprobar un pago para una reserva cancelada.");

                    payment.MarkAsPaid();

                    if (payment.Reservation.Status == ReservationStatus.Pending)
                        payment.Reservation.Confirm();
                    break;

                case PaymentStatus.Failed:
                    payment.MarkAsFailed();
                    break;

                case PaymentStatus.Cancelled:
                    payment.MarkAsCancelled();
                    break;
            }

            try
            {
                await _paymentsRepository.UpdateAsync(payment);
                await _unitOfWork.CommitAsync();
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
    }
}
