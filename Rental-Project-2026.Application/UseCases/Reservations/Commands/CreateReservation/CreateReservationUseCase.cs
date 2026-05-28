using Rental_Project_2026.Application.Contracts.Repositories;
using Rental_Project_2026.Domain.Entities;
using Rental_Project_2026.Domain.Enums;
using Rental_Project_2026.Domain.Exceptions;

namespace Rental_Project_2026.Application.UseCases.Reservations.Commands.CreateReservation
{
    public class CreateReservationUseCase : IRequestHandler<CreateReservationCommand, Guid>
    {
        private readonly IReservationsRepository _reservationsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVehiclesRepository _vehiclesRepository;

        public CreateReservationUseCase(
            IReservationsRepository reservationsRepository,
            IUnitOfWork unitOfWork,
            IVehiclesRepository vehiclesRepository)
        {
            _reservationsRepository = reservationsRepository;
            _unitOfWork = unitOfWork;
            _vehiclesRepository = vehiclesRepository;
        }

        public async Task<Guid> Handle(CreateReservationCommand command)
        {
            Vehicle? vehicle = await _vehiclesRepository.GetByIdAsync(command.VehicleId);

            if (vehicle is null)
                throw new BusinessRulesException("El vehículo no existe.");

            if (vehicle.Status != VehicleStatus.Available)
                throw new BusinessRulesException("El vehículo no se encuentra disponible para reservar.");

            bool existsActiveReservation =
                await _reservationsRepository.ExistsActiveReservationAsync(
                    command.VehicleId,
                    command.RentDate.Date,
                    command.ReturnDate.Date);

            if (existsActiveReservation)
                throw new BusinessRulesException("El vehículo ya tiene una reserva activa en las fechas seleccionadas.");

            Reservation reservation = new Reservation(
                command.VehicleId,
                vehicle.BranchId,
                command.UserId,
                command.RentDate,
                command.ReturnDate,
                vehicle.DailyPrice,
                command.CustomerFullName,
                command.DocumentNumber,
                command.PhoneNumber,
                command.Email,
                command.BirthDate,
                command.DriverLicenseCategories,
                command.DriverLicenseExpirationDate,
                command.RequiresSpecialAssistance,
                command.AssistanceNotes);

            try
            {
                Reservation newReservation = await _reservationsRepository.CreateAsync(reservation);

                await _unitOfWork.CommitAsync();

                return newReservation.Id;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
    }
}