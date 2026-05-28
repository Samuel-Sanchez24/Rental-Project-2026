using Rental_Project_2026.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Reservations.Commands.UpdateReservation
{
    public class UpdateReservationCommand : IRequest
    {
        public Guid Id { get; set; }

        public DateTime RentDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public ReservationStatus Status { get; set; }
    }
}
