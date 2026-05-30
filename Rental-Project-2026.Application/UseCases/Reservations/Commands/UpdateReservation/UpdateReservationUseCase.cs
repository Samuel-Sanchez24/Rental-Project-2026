using Rental_Project_2026.Application.Contracts.Repositories;
using Rental_Project_2026.Domain.Entities;
using Rental_Project_2026.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Reservations.Commands.UpdateReservation
{
    public class UpdateReservationUseCase : IRequestHandler<UpdateReservationCommand>   
    {
        private readonly IReservationsRepository _reservationsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateReservationUseCase(
            IReservationsRepository reservationsRepository,
            IUnitOfWork unitOfWork)
        {
            _reservationsRepository = reservationsRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handler(UpdateReservationCommand command)
        {
            Reservation? reservation = await _reservationsRepository.GetByIdAsync(command.Id);

            if (reservation is null)
                throw new BusinessRulesException("La reserva no existe.");

            bool datesChanged = reservation.RentDate.Date != command.RentDate.Date ||
                                reservation.ReturnDate.Date != command.ReturnDate.Date;

            if (datesChanged)
            {
                bool existsActiveReservation = await _reservationsRepository.ExistsActiveReservationAsync(
                    reservation.VehicleId,
                    command.RentDate.Date,
                    command.ReturnDate.Date,
                    reservation.Id);

                if (existsActiveReservation)
                    throw new BusinessRulesException("El vehículo ya tiene una reserva activa en las fechas seleccionadas.");

                reservation.UpdateDates(command.RentDate, command.ReturnDate);
            }

            if (command.IsAdmin)
            {
                reservation.UpdateCustomerInformation(
                    command.CustomerFullName,
                    command.DocumentNumber,
                    command.PhoneNumber,
                    command.Email,
                    command.BirthDate,
                    command.DriverLicenseCategories,
                    command.DriverLicenseExpirationDate,
                    command.RequiresSpecialAssistance,
                    command.AssistanceNotes);

                reservation.ChangeStatus(command.Status);
            }

            try
            {
                await _reservationsRepository.UpdateAsync(reservation);
                await _unitOfWork.CommitAsync();
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
    }
}
