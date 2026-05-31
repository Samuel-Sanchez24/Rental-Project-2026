using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Account.Queries.GetAccessibleBranches
{
    public class GetAccessibleBranchesQuery : IRequest<IReadOnlyList<AccessibleBranchItemDTO>>
    {
        public required string UserId { get; init; }
    }
}
