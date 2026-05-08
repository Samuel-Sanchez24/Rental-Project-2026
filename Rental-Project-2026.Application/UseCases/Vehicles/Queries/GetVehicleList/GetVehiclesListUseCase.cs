using Rental_Project_2026.Application.Contracts.Pagination;
using Rental_Project_2026.Application.Contracts.Repositories;
using Rental_Project_2026.Domain.Entities;

namespace Rental_Project_2026.Application.UseCases.Vehicles.Queries.GetVehicleList
{
    public class GetVehiclesListUseCase : IRequestHandler<GetVehiclesListQuery, PaginationResponse<VehicleListItemDTO>>
    {
        private readonly IVehiclesRepository _vehiclesRepository;
        public GetVehiclesListUseCase(IVehiclesRepository vehiclesRepository)
        {
            _vehiclesRepository = vehiclesRepository;
        }
        public async Task<PaginationResponse<VehicleListItemDTO>> Handle(GetVehiclesListQuery query)
        {
            PaginationResponse<Vehicle> pagedVehicles = await _vehiclesRepository.GetPagedList(
                query.Pagination,
                query.PlateFilter,
                query.ModelFilter,
                query.BrandFilter,
                query.ColorFilter,
                query.YearFilter,
                query.DailyPriceFilter,
                query.StatusFilter,
                CancellationToken.None);

            List<VehicleListItemDTO> itemsDTO = pagedVehicles.Items
                .Select(v => v.ToDTO())
                .ToList();
            return PaginationResponse<VehicleListItemDTO>.Create(itemsDTO, pagedVehicles.TotalCount, query.Pagination);
        }
    }
}
