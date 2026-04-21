using Rental_Project_2026.Application.Contracts.Repositories;
using Rental_Project_2026.Domain.Entities.Branches;

namespace Rental_Project_2026.Persistence.Repositories
{
    public class BranchesRepository : Repository<Branch>, IBranchesRepository
    {
        public BranchesRepository(DataContext context) : base(context)
        {
        }
    }
}
