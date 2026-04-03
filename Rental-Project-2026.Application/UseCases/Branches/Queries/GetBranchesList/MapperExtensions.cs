using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Branches.Queries.GetBranchesList
{
    internal static class MapperExtensions
    {
        public static BranchListItemDTO ToDTO(this Branch branch)
        {
            return new BranchListItemDTO
            {
                Id = branch.Id,
                Name = branch.Name,
                City = branch.City,
                Address = branch.Address,
                Phone = branch.Phone,
                Status = branch.Status
            };
        }
    }
}
