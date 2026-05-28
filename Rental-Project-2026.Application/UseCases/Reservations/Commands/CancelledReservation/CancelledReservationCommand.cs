using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Reservations.Commands.CancelledReservation
{
    public class CancelledReservationCommand : IRequest
    {
        public Guid Id { get; set; }

        public CancelledReservationCommand(Guid id)
        {
            Id = id;
        }
    }
}
