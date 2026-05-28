using Rental_Project_2026.Application.Contracts.Repositories;
using Rental_Project_2026.Domain.Entities;
using Rental_Project_2026.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Reservations.Commands.CancelledReservation
{
    public class CancelledReservationUseCase : IRequestHandler<CancelledReservationCommand>
    {
        private readonly IReservationsRepository _reservationsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CancelledReservationUseCase(
            IReservationsRepository reservationsRepository,
            IUnitOfWork unitOfWork)
        {
            _reservationsRepository = reservationsRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handler(CancelledReservationCommand command)
        {
            Reservation? reservation = await _reservationsRepository.GetByIdAsync(command.Id);

            if (reservation is null)
                throw new BusinessRulesException("La reserva no existe.");

            reservation.Cancel();

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
