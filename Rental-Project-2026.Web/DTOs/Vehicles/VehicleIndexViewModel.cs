using Rental_Project_2026.Application.Contracts.Pagination;
using Rental_Project_2026.Application.UseCases.Vehicles.Queries.GetVehicleList;
using Rental_Project_2026.Domain.Enums;

namespace Rental_Project_2026.Web.DTOs.Vehicles
{
    public class VehicleIndexViewModel
    {
        public required PaginationResponse<VehicleListItemDTO> List { get; init; }

        public string FilterBrand { get; set;} = string.Empty;
        public string FilterColor { get; set;} = string.Empty;
        public VehicleStatus? FilterStatus { get; set; }
    }
}
