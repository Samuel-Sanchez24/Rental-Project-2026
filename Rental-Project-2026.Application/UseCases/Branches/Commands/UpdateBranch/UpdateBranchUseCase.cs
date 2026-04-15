using Rental_Project_2026.Application.Contracts.Repositories;
using Rental_Project_2026.Domain.Entities.Branches;
using Rental_Project_2026.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Branches.Commands.UpdateBranch
{
    public class UpdateBranchUseCase : IRequestHandler<UpdateBranchCommand>
    {
        private readonly IBranchesRepository _branchesRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBranchUseCase(IBranchesRepository branchesRepository, IUnitOfWork unitOfWork)
        {
            _branchesRepository = branchesRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Handler(UpdateBranchCommand command)
        {
            Branch? branch = await _branchesRepository.GetByIdAsync(command.Id);
            if (branch == null)
            {
                throw new BusinessRulesException("La sucursal no existe.");
            }
            
            branch.UpdateBranch(command.Name, command.City, command.Address, command.Phone);

            if (command.Status == BranchStatus.Active)
            {
                branch.Activate();

            }
            else
            {
                branch.Deactivate();
            }
            try
            {
                await _branchesRepository.UpdateAsync(branch);
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
