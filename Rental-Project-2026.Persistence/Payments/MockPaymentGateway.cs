using Microsoft.Extensions.Options;
using Rental_Project_2026.Application.Contracts.Payments;
using Rental_Project_2026.Domain.Enums;

namespace Rental_Project_2026.Persistence.Payments
{
    public class MockPaymentGateway : IPaymentGateway
    {
        private readonly PaymentGatewayOptions _options;

        public MockPaymentGateway(IOptions<PaymentGatewayOptions> options)
        {
            _options = options.Value;
        }

        public string ProviderName => "Mock";

        public Task<PaymentGatewayCreateResponse> CreatePaymentAsync(
            PaymentGatewayCreateRequest request,
            CancellationToken cancellationToken = default)
        {
            string checkoutBasePath = string.IsNullOrWhiteSpace(_options.Mock.CheckoutBasePath)
                ? "/Payments/Checkout"
                : _options.Mock.CheckoutBasePath.TrimEnd('/');

            return Task.FromResult(new PaymentGatewayCreateResponse
            {
                ProviderReference = $"MOCK-{request.PaymentId:N}",
                PaymentUrl = $"{checkoutBasePath}?paymentId={request.PaymentId}",
                Status = PaymentStatus.Pending,
                Message = "Pago mock creado exitosamente."
            });
        }

        public Task<PaymentGatewayStatusResponse> GetPaymentStatusAsync(
            string providerReference,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(new PaymentGatewayStatusResponse
            {
                ProviderReference = providerReference,
                Status = PaymentStatus.Pending,
                Message = "Consulta mock sin proveedor externo."
            });
        }

        public Task<bool> ValidateWebhookAsync(
            PaymentGatewayWebhookValidationRequest request,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(true);
        }
    }
}
