using Rental_Project_2026.Application.Contracts.Repositories;
using Rental_Project_2026.Domain.Entities;
using Rental_Project_2026.Domain.Enums;
using Rental_Project_2026.Domain.Exceptions;

namespace Rental_Project_2026.Application.UseCases.Vehicles.Commands.UpdateVehicle
{
    public class UpdateVehicleUseCase : IRequestHandler<UpdateVehicleCommand>
    {
        private readonly IVehiclesRepository _vehiclesRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateVehicleUseCase(IVehiclesRepository vehiclesRepository, IUnitOfWork unitOfWork)
        {
            _vehiclesRepository = vehiclesRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handler(UpdateVehicleCommand command)
        {
            Vehicle? vehicle = await _vehiclesRepository.GetByIdAsync(command.Id);
            if (vehicle == null)
            {
                throw new BusinessRulesException("El vehículo no existe.");
            }
            vehicle.UpdateVehicle(
                command.Plate, 
                command.Model,
                command.Brand,
                command.Year,
                command.Color,
                command.DailyPrice, 
                command.Status,
                command.BranchId);

            switch (command.Status)
            {
                case VehicleStatus.Available:
                    vehicle.MarkAsEnable();
                    break;

                case VehicleStatus.Inactive:
                    vehicle.MarkAsInactive();
                    break;

                case VehicleStatus.Rented:
                    vehicle.MarkAsRented();
                    break; 

                case VehicleStatus.Maintenance:
                    vehicle.MarkAsMaintenance();
                    break;

                default:
                    throw new BusinessRulesException("El estado del vehiculo no es valido.");
            }

            try
            {
                await _vehiclesRepository.UpdateAsync(vehicle);
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
