using Rental_Project_2026.Application.Contracts.Repositories;
using Rental_Project_2026.Domain.Entities;
using Rental_Project_2026.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Vehicles.Queries.GetVehicleById
{
    public class GetVehicleByIdUseCase : IRequestHandler<GetVehicleByIdQuery, VehicleDetailDTO>
    {
        private readonly IVehiclesRepository _vehiclesRepository;
        
        public GetVehicleByIdUseCase(IVehiclesRepository vehiclesRepository)
        {
            _vehiclesRepository = vehiclesRepository;
        }

        public async Task<VehicleDetailDTO> Handle(GetVehicleByIdQuery request)
        {
            Vehicle? vehicle = await _vehiclesRepository.GetVehicleDetailByIdAsync(request.Id);
            if (vehicle == null)
            {
                throw new BusinessRulesException("El vehículo no existe.");
            }
            return new VehicleDetailDTO
            {
                Id = vehicle.Id,
                Plate = vehicle.Plate,
                Model = vehicle.Model,
                Brand = vehicle.Brand,
                Color = vehicle.Color,
                Year = vehicle.Year,
                DailyPrice = vehicle.DailyPrice,
                Status = vehicle.Status,
                BranchId = vehicle.BranchId,

                ImageUrl = vehicle.ImageUrl,
                BranchName = vehicle.Branch != null ? vehicle.Branch.Name : string.Empty,
                BranchCity = vehicle.Branch != null ? vehicle.Branch.City : string.Empty,
                BranchAddress = vehicle.Branch != null ? vehicle.Branch.Address : string.Empty
            };
        }
    }
}
