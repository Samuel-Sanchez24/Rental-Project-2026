using Rental_Project_2026.Application.Contracts.Repositories;
using Rental_Project_2026.Domain.Entities;
using Rental_Project_2026.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Reservations.Queries.GetReservationById
{
    public class GetReservationByIdUseCase : IRequestHandler<GetReservationByIdQuery, ReservationDetailDTO>
    {
        private readonly IReservationsRepository _reservationsRepository;

        public GetReservationByIdUseCase(IReservationsRepository reservationsRepository)
        {
            _reservationsRepository = reservationsRepository;
        }

        public async Task<ReservationDetailDTO> Handle(GetReservationByIdQuery request)
        {
            Reservation? reservation = await _reservationsRepository.GetReservationByIdAsync(request.Id);

            if (reservation is null)
                throw new BusinessRulesException("La reserva no existe.");

            return new ReservationDetailDTO
            {
                Id = reservation.Id,

                RentDate = reservation.RentDate,
                ReturnDate = reservation.ReturnDate,

                Days = reservation.Days,

                DailyPriceAtBooking = reservation.DailyPrice,
                TotalPrice = reservation.TotalPrice,

                Status = reservation.Status,

                CreatedAt = reservation.CreatedAt,


                CustomerFullName = reservation.CustomerFullName,
                DocumentNumber = reservation.DocumentNumber,
                PhoneNumber = reservation.PhoneNumber,
                Email = reservation.Email,
                BirthDate = reservation.BirthDate,

                DriverLicenseCategories = reservation.DriverLicenseCategories,
                DriverLicenseExpirationDate = reservation.DriverLicenseExpirationDate,

                RequiresSpecialAssistance = reservation.RequiresSpecialAssistance,
                AssistanceNotes = reservation.AssistanceNotes,

                VehicleId = reservation.VehicleId,
                VehiclePlate = reservation.Vehicle != null ? reservation.Vehicle.Plate : string.Empty,
                VehicleBrand = reservation.Vehicle != null ? reservation.Vehicle.Brand : string.Empty,
                VehicleModel = reservation.Vehicle != null ? reservation.Vehicle.Model : string.Empty,
                VehicleImageUrl = reservation.Vehicle != null ? reservation.Vehicle.ImageUrl : null,

                BranchId = reservation.BranchId,
                BranchName = reservation.Branch != null ? reservation.Branch.Name : string.Empty,
                BranchCity = reservation.Branch != null ? reservation.Branch.City : string.Empty,
                BranchAddress = reservation.Branch != null ? reservation.Branch.Address : string.Empty,

                UserId = reservation.UserId,
                UserFullName = reservation.CustomerFullName,
                UserEmail = reservation.Email,
            };
        }
    }
}
