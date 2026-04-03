using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Application.UseCases.Branches.Queries.GetBranchById
{
    public class GetBranchByIdQuery : IRequest<BranchDetailDTO>
    {
        public readonly Guid Id;

        public GetBranchByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
