using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Account.Queries.GetVehicleById
{
    public class GetVehicleByIdQuery : IRequest<AccessibleVehicleDatailDTO> 
    {
        public required string UserId { get; set; }
        public Guid VehicleId { get; set; }
    }
}
