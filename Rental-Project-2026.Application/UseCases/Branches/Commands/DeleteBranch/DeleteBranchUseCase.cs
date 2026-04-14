using Rental_Project_2026.Application.Contracts.Repositories;
using Rental_Project_2026.Application.Utilities.Mediator;
using Rental_Project_2026.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Branches.Commands.DeleteBranch
{
    public class DeleteBranchUseCase : IRequestHandler<DeleteBranchCommand>
    {
        private IBranchesRepository _repository;
        private IUnitOfWork _unitOfWork;

        public DeleteBranchUseCase(IBranchesRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handler(DeleteBranchCommand command)
        {   
            Domain.Entities.Branches.Branch?  branch = await _repository.GetByIdAsync(command.Id);

            if (branch is null) 
            {
                throw new BusinessRulesException ($"No existe Rama con id'{command.Id}'");
            }

            // TODO: Validar que no tenga articulos asociados

            try
            {
                await _repository.DeleteAsync(branch);
                await _unitOfWork.CommitAsync();
            }
            catch
            {
               await _unitOfWork.RollbackAsync ();
                throw;
            }

            
        }
    }
}
