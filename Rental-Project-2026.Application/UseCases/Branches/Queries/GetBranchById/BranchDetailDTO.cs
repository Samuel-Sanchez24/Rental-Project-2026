using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Branches.Queries.GetBranchById
{
    public class BranchDetailDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public BranchStatus Status { get; set; }
    }
}
