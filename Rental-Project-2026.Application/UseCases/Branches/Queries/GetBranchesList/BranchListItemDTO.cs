using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Branches.Queries.GetBranchesList
{
    public class BranchListItemDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public  BranchStatus Status { get; set; }
    }
}
