using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Account.Queries.GetAccessibleBranches
{
    public class AccessibleBranchItemDTO
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
    }
}
