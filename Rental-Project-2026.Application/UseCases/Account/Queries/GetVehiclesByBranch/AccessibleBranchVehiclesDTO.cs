using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Account.Queries.GetVehiclesByBranch
{
    public class AccessibleBranchVehiclesDTO
    {
        public Guid BranchId { get; init; }
        public string BranchName { get; init; } = string.Empty;
        public IReadOnlyList<AccessibleVehiclesListItemDTO> Vehicles { get; init; } = Array.Empty<AccessibleVehiclesListItemDTO>();
    }
}
