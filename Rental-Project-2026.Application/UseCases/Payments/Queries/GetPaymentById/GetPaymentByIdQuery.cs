namespace Rental_Project_2026.Application.UseCases.Payments.Queries.GetPaymentById
{
    public class GetPaymentByIdQuery : IRequest<PaymentDetailDTO>
    {
        public GetPaymentByIdQuery(Guid paymentId)
        {
            PaymentId = paymentId;
        }

        public Guid PaymentId { get; }
    }
}
