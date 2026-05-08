using Rental_Project_2026.Application.Contracts.Repositories;
using Rental_Project_2026.Domain.Entities;
using Rental_Project_2026.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Vehicles.Commands.DeleteVehicle
{
    public class DeleteVehicleUseCase : IRequestHandler<DeleteVehicleCommand>
    {
        private readonly IVehiclesRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteVehicleUseCase(IVehiclesRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handler(DeleteVehicleCommand command)
        {
            Vehicle? vehicle = await _repository.GetByIdAsync(command.Id);
            if (vehicle == null)
            {
                throw new BusinessRulesException($"No existe un vehículo con el id {command.Id}");
            }

            try
            {
                await _repository.DeleteAsync(vehicle);
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
