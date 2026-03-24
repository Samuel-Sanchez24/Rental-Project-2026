using System;
using System.Collections.Generic;
using System.Text;
using Rental_Project_2026.Application.Contracts.Repositories;
using static System.Collections.Specialized.BitVector32;

namespace Rental_Project_2026.Application.UseCases.Branches.Commands
{
    public class CreateBranchUseCase : IRequestHandler<CreateBranchCommand, Guid>
    {
        private readonly IBranchesRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateBranchUseCase(IBranchesRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }



        public async Task<Guid> Handle(CreateBranchCommand command)
        {
            Branch branch = new Branch(command.Name, command.City,command.Address,command.Phone);
            try
            {
                Branch newBranch = await _repository.CreateAsync(branch);
                await _unitOfWork.CommitAsync();
                return newBranch.Id;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
    }
}
