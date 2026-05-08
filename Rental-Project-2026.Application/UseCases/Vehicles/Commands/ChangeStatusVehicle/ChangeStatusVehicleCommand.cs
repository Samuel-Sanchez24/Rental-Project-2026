using Rental_Project_2026.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Vehicles.Commands.ChangeStatusVehicle
{
    public class ChangeStatusVehicleCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public VehicleStatus Status { get; set; }

        public ChangeStatusVehicleCommand(Guid id, VehicleStatus status)
        {
            Id = id;
            Status = status;
        }
    }
}
