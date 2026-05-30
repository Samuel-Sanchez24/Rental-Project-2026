namespace Rental_Project_2026.Persistence.Payments
{
    public class PaymentGatewayOptions
    {
        public const string SectionName = "PaymentGateways";

        public string DefaultGateway { get; set; } = "Mock";
        public BancolombiaGatewayOptions Bancolombia { get; set; } = new();
        public MockGatewayOptions Mock { get; set; } = new();
    }

    public class BancolombiaGatewayOptions
    {
        public string BaseUrl { get; set; } = string.Empty;
        public string ClientId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;
        public string RedirectUrl { get; set; } = string.Empty;
        public string WebhookUrl { get; set; } = string.Empty;
        public string Environment { get; set; } = "Sandbox";
    }

    public class MockGatewayOptions
    {
        public string CheckoutBasePath { get; set; } = "/Payments/Checkout";
    }
}
