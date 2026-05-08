using Rental_Project_2026.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Vehicles.Queries.GetVehicleList
{
    internal static class MapperExtensions
    {
        public static VehicleListItemDTO ToDTO(this Vehicle vehicle)
        {
            return new VehicleListItemDTO
            {
                Id = vehicle.Id,
                Plate = vehicle.Plate,
                Model = vehicle.Model,
                Brand = vehicle.Brand,
                Color = vehicle.Color,
                Year = vehicle.Year,
                DailyPrice = vehicle.DailyPrice,
                Status = vehicle.Status
            };
        }
    }
}
