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
