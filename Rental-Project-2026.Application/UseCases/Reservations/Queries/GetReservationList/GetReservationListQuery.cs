using Rental_Project_2026.Application.Contracts.Pagination;
using Rental_Project_2026.Domain.Enums;

namespace Rental_Project_2026.Application.UseCases.Reservations.Queries.GetReservationList
{
    public class GetReservationListQuery : IRequest<PaginationResponse<ReservationListItemDTO>>
    {
        public PaginationRequest Pagination { get; set; } = PaginationRequest.Normalized();

        public string? UserIdFilter { get; set; }

        public Guid? VehicleIdFilter { get; set; }

        public Guid? BranchIdFilter { get; set; }

        public ReservationStatus? StatusFilter { get; set; }

        public DateTime? RentDateFromFilter { get; set; }

        public DateTime? RentDateToFilter { get; set; }
    }
}
