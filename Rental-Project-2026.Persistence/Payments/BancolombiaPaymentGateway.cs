using Microsoft.Extensions.Options;
using Rental_Project_2026.Application.Contracts.Payments;

namespace Rental_Project_2026.Persistence.Payments
{
    public class BancolombiaPaymentGateway : IPaymentGateway
    {
        private readonly HttpClient _httpClient;
        private readonly BancolombiaGatewayOptions _options;

        public BancolombiaPaymentGateway(
            HttpClient httpClient,
            IOptions<PaymentGatewayOptions> options)
        {
            _httpClient = httpClient;
            _options = options.Value.Bancolombia;
        }

        public string ProviderName => "Bancolombia";

        public Task<PaymentGatewayCreateResponse> CreatePaymentAsync(
            PaymentGatewayCreateRequest request,
            CancellationToken cancellationToken = default)
        {
            EnsureConfigured();

            throw new NotSupportedException(
                "El adaptador de Bancolombia quedó preparado para configuración segura, " +
                "pero requiere el contrato oficial del proveedor antes de habilitarse en producción.");
        }

        public Task<PaymentGatewayStatusResponse> GetPaymentStatusAsync(
            string providerReference,
            CancellationToken cancellationToken = default)
        {
            EnsureConfigured();

            throw new NotSupportedException(
                "La consulta de estados de Bancolombia requiere el contrato oficial del proveedor.");
        }

        public Task<bool> ValidateWebhookAsync(
            PaymentGatewayWebhookValidationRequest request,
            CancellationToken cancellationToken = default)
        {
            EnsureConfigured();

            throw new NotSupportedException(
                "La validación de webhooks de Bancolombia requiere el contrato oficial del proveedor.");
        }

        private void EnsureConfigured()
        {
            if (string.IsNullOrWhiteSpace(_options.BaseUrl) ||
                string.IsNullOrWhiteSpace(_options.ClientId) ||
                string.IsNullOrWhiteSpace(_options.ClientSecret) ||
                string.IsNullOrWhiteSpace(_options.RedirectUrl) ||
                string.IsNullOrWhiteSpace(_options.WebhookUrl))
            {
                throw new InvalidOperationException(
                    "La configuración de Bancolombia está incompleta. Completa PaymentGateways:Bancolombia " +
                    "o usa PaymentGateways:DefaultGateway = Mock.");
            }

            _httpClient.BaseAddress = new Uri(_options.BaseUrl);
        }
    }
}
