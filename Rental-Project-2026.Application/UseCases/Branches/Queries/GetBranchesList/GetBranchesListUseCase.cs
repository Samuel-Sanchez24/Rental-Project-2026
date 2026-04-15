using System;
using System.Collections.Generic;
using System.Text;
using Rental_Project_2026.Application.Contracts.Repositories;
using Rental_Project_2026.Application.Utilities.Mediator;
using Rental_Project_2026.Domain.Entities.Branches;

namespace Rental_Project_2026.Application.UseCases.Branches.Queries.GetBranchesList
{
    public class GetBranchesListUseCase : IRequestHandler<GetBranchesListQuery, IEnumerable<BranchListItemDTO>>
    {
        private readonly IBranchesRepository _branchesRepository;

        public GetBranchesListUseCase(IBranchesRepository branchesRepository)
        {
            _branchesRepository = branchesRepository;
        }

        public async Task<IEnumerable<BranchListItemDTO>> Handle(GetBranchesListQuery request)
        {
            IEnumerable<Branch> branches = await _branchesRepository.GetListAsync();

            List<BranchListItemDTO> branchesDTO = branches.Select(b => b.ToDTO())
                                                          .ToList();
            return branchesDTO;
        }
    }
}   
