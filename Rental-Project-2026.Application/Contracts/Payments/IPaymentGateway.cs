namespace Rental_Project_2026.Application.Contracts.Payments
{
    public interface IPaymentGateway
    {
        string ProviderName { get; }

        Task<PaymentGatewayCreateResponse> CreatePaymentAsync(
            PaymentGatewayCreateRequest request,
            CancellationToken cancellationToken = default);

        Task<PaymentGatewayStatusResponse> GetPaymentStatusAsync(
            string providerReference,
            CancellationToken cancellationToken = default);

        Task<bool> ValidateWebhookAsync(
            PaymentGatewayWebhookValidationRequest request,
            CancellationToken cancellationToken = default);
    }
}
