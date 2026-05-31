using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Account.Queries.GetVehicleById
{
    public class AccessibleVehicleDatailDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public Guid BranchId { get; init; }
        public string BranchName { get; init; } = string.Empty;

    }
}
