using Rental_Project_2026.Application.Contracts.Repositories;
using Rental_Project_2026.Domain.Entities;
using Rental_Project_2026.Domain.Exceptions;

namespace Rental_Project_2026.Application.UseCases.Payments.Queries.GetPaymentById
{
    public class GetPaymentByIdUseCase : IRequestHandler<GetPaymentByIdQuery, PaymentDetailDTO>
    {
        private readonly IPaymentsRepository _paymentsRepository;

        public GetPaymentByIdUseCase(IPaymentsRepository paymentsRepository)
        {
            _paymentsRepository = paymentsRepository;
        }

        public async Task<PaymentDetailDTO> Handle(GetPaymentByIdQuery request)
        {
            Payment? payment = await _paymentsRepository.GetPaymentByIdAsync(request.PaymentId);

            if (payment is null)
                throw new BusinessRulesException("El pago no existe.");

            return new PaymentDetailDTO
            {
                Id = payment.Id,
                ReservationId = payment.ReservationId,
                UserId = payment.Reservation.UserId,
                Amount = payment.Amount,
                Status = payment.Status,
                Provider = payment.Provider,
                ProviderReference = payment.ProviderReference,
                PaymentUrl = payment.PaymentUrl,
                CreatedAt = payment.CreatedAt,
                UpdatedAt = payment.UpdatedAt,
                PaidAt = payment.PaidAt,
                ReservationStatus = payment.Reservation.Status,
                VehiclePlate = payment.Reservation.Vehicle?.Plate ?? string.Empty,
                VehicleBrand = payment.Reservation.Vehicle?.Brand ?? string.Empty,
                VehicleModel = payment.Reservation.Vehicle?.Model ?? string.Empty,
                BranchName = payment.Reservation.Branch?.Name ?? string.Empty,
                CustomerFullName = payment.Reservation.CustomerFullName,
                CustomerEmail = payment.Reservation.Email,
                IsMockProvider = string.Equals(payment.Provider, "Mock", StringComparison.OrdinalIgnoreCase)
            };
        }
    }
}
