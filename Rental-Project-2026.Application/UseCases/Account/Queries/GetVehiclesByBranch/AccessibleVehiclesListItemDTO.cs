using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Account.Queries.GetVehiclesByBranch
{
    public class AccessibleVehiclesListItemDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

    }
}
