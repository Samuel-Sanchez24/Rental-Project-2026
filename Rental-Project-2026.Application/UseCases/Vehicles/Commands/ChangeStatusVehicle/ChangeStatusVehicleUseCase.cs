using Rental_Project_2026.Application.Contracts.Repositories;
using Rental_Project_2026.Domain.Entities;
using Rental_Project_2026.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Vehicles.Commands.ChangeStatusVehicle
{
    public class ChangeStatusVehicleUseCase: IRequestHandler<ChangeStatusVehicleCommand, Guid>
    {
        private readonly IVehiclesRepository _vehiclesRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ChangeStatusVehicleUseCase(IVehiclesRepository vehiclesRepository, IUnitOfWork unitOfWork)
        {
            _vehiclesRepository = vehiclesRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(ChangeStatusVehicleCommand command)
        {
            Vehicle? vehicle = await _vehiclesRepository.GetByIdAsync(command.Id);
            
            if(vehicle == null)
            {
                throw new BusinessRulesException("El vehiculo no existe.");
            }
            vehicle.ChangeStatus(command.Status);

            await _vehiclesRepository.UpdateAsync(vehicle);
            await _unitOfWork.CommitAsync();

            return vehicle.Id;
        }
    }
}
