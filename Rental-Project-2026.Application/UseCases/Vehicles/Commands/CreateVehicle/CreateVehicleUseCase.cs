using Rental_Project_2026.Application.Contracts.Repositories;
using Rental_Project_2026.Domain.Entities;

namespace Rental_Project_2026.Application.UseCases.Vehicles.Commands.CreateVehicle
{
    internal class CreateVehicleUseCase : IRequestHandler<CreateVehicleCommand, Guid>
    {
        private readonly IVehiclesRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateVehicleUseCase(IVehiclesRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateVehicleCommand command)
        {
            Vehicle vehicle = new Vehicle(
                command.Plate,
                command.Model,
                command.Brand,
                command.Color,
                command.Year,
                command.DailyPrice,
                command.Status,
                command.BranchId,
                command.ImageUrl);
            try
            {
                Vehicle newVehicle = await _repository.CreateAsync(vehicle);
                await _unitOfWork.CommitAsync();
                return newVehicle.Id;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
    }
}
