using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Reservations.Queries.GetReservationById
{
    public class GetReservationByIdQuery : IRequest<ReservationDetailDTO>
    {
        public readonly Guid Id;

        public GetReservationByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
