using Rental_Project_2026.Application.Contracts.Pagination;
using Rental_Project_2026.Domain.Enums;

namespace Rental_Project_2026.Application.UseCases.Vehicles.Queries.GetVehicleList
{
    public class GetVehiclesListQuery : IRequest<PaginationResponse<VehicleListItemDTO>>
    {
        public PaginationRequest Pagination { get; set; } = PaginationRequest.Normalized();

        public string? PlateFilter { get; set; }
        public string? ModelFilter { get; set; }
        public string? BrandFilter { get; set; }
        public string? ColorFilter { get; set; }
        public int? YearFilter { get; set; }
        public decimal? DailyPriceFilter { get; set; }
        public VehicleStatus? StatusFilter { get; set; }
    }
}
