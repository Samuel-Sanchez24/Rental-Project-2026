using Rental_Project_2026.Application.Contracts.Repositories;
using Rental_Project_2026.Application.UseCases.Branches.Commands.DeleteBranch;
using Rental_Project_2026.Application.Utilities.Mediator;
using Rental_Project_2026.Domain.Entities.Branches;
using Rental_Project_2026.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Branches.Commands.ActiveBranch
{
    public class ActivateBranchUseCase : IRequestHandler<ActivateBranchCommand>
    {
        private IBranchesRepository _repository;
        private IUnitOfWork _unitOfWork;

        public ActivateBranchUseCase(IBranchesRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handler(ActivateBranchCommand command)
        {
            Branch? branch = await _repository.GetByIdAsync(command.Id);

            if (branch is null)
            {
                throw new BusinessRulesException($"No existe Rama con id'{command.Id}'");
            }

            try
            {
                branch.Activate();
                await _repository.UpdateAsync(branch);
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

