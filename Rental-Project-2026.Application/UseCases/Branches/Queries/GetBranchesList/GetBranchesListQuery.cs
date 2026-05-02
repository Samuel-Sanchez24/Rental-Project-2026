using Rental_Project_2026.Application.Contracts.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Branches.Queries.GetBranchesList
{
    public class GetBranchesListQuery : IRequest<PaginationResponse<BranchListItemDTO>>
    {
        public PaginationRequest Pagination { get; set; } = PaginationRequest.Normalized();
        public string ? NameFilter { get; set; }
        public string? CityFilter { get; set; }
        public string? AddressFilter { get; set; }
        public string ? PhoneNumberFilter { get; set; }
        public BranchStatus? StatusFilter { get; set; }
    }   
}
