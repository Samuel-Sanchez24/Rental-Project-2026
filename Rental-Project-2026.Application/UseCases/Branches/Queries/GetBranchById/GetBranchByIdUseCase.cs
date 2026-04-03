using Rental_Project_2026.Application.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Branches.Queries.GetBranchById
{
    public class GetBranchByIdUseCase : IRequestHandler<GetBranchByIdQuery, BranchDetailDTO>
    {
        private readonly IBranchesRepository _branchesRepository;
        public GetBranchByIdUseCase(IBranchesRepository branchesRepository)
        {
            _branchesRepository = branchesRepository;
        }
        public async Task<BranchDetailDTO> Handle(GetBranchByIdQuery request)
        {
            Branch? branches = await _branchesRepository.GetByIdAsync(request.Id);
            if (branches == null)
            {
                throw new BusinessRulesException("La sucursal no existe.");
            }
            return new BranchDetailDTO
            {
                Id = branches.Id,
                Name = branches.Name,
                City = branches.City,
                Address = branches.Address,
                Phone = branches.Phone,
                Status = branches.Status
            };


        }
    }
}
