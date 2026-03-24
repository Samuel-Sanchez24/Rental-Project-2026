using Rental_Project_2026.Application.Contracts.Repositories;

namespace Rental_Project_2026.Persistence.Repositories
{
    public class BranchesRepository : Repository<Branch>, IBranchesRepository
    {
        public BranchesRepository(DataContext context) : base(context)
        {
        }
    }
}
