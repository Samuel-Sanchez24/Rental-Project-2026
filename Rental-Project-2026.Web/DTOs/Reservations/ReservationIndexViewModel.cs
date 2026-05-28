using Rental_Project_2026.Application.Contracts.Pagination;
using Rental_Project_2026.Application.UseCases.Reservations.Queries.GetReservationList;
using Rental_Project_2026.Domain.Enums;

namespace Rental_Project_2026.Web.DTOs.Reservations
{
    public class ReservationIndexViewModel
    {
        public required PaginationResponse<ReservationListItemDTO> List { get; init; } 

        public string? UserIdFilter { get; set; }

        public Guid? VehicleIdFilter { get; set; }

        public Guid? BranchIdFilter { get; set; }

        public ReservationStatus? StatusFilter { get; set; }

        public DateTime? RentDateFromFilter { get; set; }

        public DateTime? RentDateToFilter { get; set; }
    }
}
