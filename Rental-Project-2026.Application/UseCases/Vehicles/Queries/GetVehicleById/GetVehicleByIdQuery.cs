using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Vehicles.Queries.GetVehicleById
{
    public class GetVehicleByIdQuery : IRequest<VehicleDetailDTO>
    {
        public readonly Guid Id;

        public GetVehicleByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
