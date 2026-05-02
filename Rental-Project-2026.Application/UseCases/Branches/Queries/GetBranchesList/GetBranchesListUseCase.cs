using System;
using System.Collections.Generic;
using System.Text;
using Rental_Project_2026.Application.Contracts.Pagination;
using Rental_Project_2026.Application.Contracts.Repositories;
using Rental_Project_2026.Application.Utilities.Mediator;
using Rental_Project_2026.Domain.Entities.Branches;

namespace Rental_Project_2026.Application.UseCases.Branches.Queries.GetBranchesList
{
    public class GetBranchesListUseCase : IRequestHandler<GetBranchesListQuery, PaginationResponse<BranchListItemDTO>>
    {
        private readonly IBranchesRepository _branchesRepository;

        public GetBranchesListUseCase(IBranchesRepository branchesRepository)
        {
            _branchesRepository = branchesRepository;
        }

        public async Task<PaginationResponse<BranchListItemDTO>> Handle(GetBranchesListQuery query)
        {
            PaginationResponse<Branch> pagedBranches = await _branchesRepository.GetPagedList(
                query.Pagination,
                query.NameFilter,
                query.CityFilter,
                query.AddressFilter,
                query.PhoneNumberFilter,
                query.StatusFilter);

            List<BranchListItemDTO> itemsDTO = pagedBranches.Items
                .Select(b => b.ToDTO())
                .ToList();
            
            return PaginationResponse<BranchListItemDTO>.Create(itemsDTO, pagedBranches.TotalCount, query.Pagination);
        }
    }
}   
