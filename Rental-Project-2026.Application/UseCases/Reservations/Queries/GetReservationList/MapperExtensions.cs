using Rental_Project_2026.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Reservations.Queries.GetReservationList
{
    public static class MapperExtensions
    {
        public static ReservationListItemDTO ToDTO(this Reservation reservation)
        {
            return new ReservationListItemDTO
            {
                Id = reservation.Id,

                RentDate = reservation.RentDate,
                ReturnDate = reservation.ReturnDate,

                Days = reservation.Days,

                TotalPrice = reservation.TotalPrice,

                Status = reservation.Status,

                CreatedAt = reservation.CreatedAt,

                VehicleId = reservation.VehicleId,
                VehiclePlate = reservation.Vehicle != null ? reservation.Vehicle.Plate : string.Empty,
                VehicleBrand = reservation.Vehicle != null ? reservation.Vehicle.Brand : string.Empty,
                VehicleModel = reservation.Vehicle != null ? reservation.Vehicle.Model : string.Empty,
                VehicleImageUrl = reservation.Vehicle != null ? reservation.Vehicle.ImageUrl : null,

                BranchId = reservation.BranchId,
                BranchName = reservation.Branch != null ? reservation.Branch.Name : string.Empty,
                BranchCity = reservation.Branch != null ? reservation.Branch.City : string.Empty,

                UserId = reservation.UserId,
                UserFullName = reservation.CustomerFullName,
                UserEmail = reservation.Email,
            };
        }
    }
}
