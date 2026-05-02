using Rental_Project_2026.Application.Contracts.Repositories;
using Rental_Project_2026.Domain.Entities.Branches;
using Microsoft.EntityFrameworkCore;
using Rental_Project_2026.Application.Contracts.Pagination;
using Rental_Project_2026.Persistence.Extensions;


namespace Rental_Project_2026.Persistence.Repositories
{
    public class BranchesRepository : Repository<Branch>, IBranchesRepository
    {
        private readonly DataContext _context;
        public BranchesRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Branch?> GetByCityAsync(string city)
        {
            return await _context.Branches.FirstOrDefaultAsync(b => b.City == city);
        }

        public async Task<PaginationResponse<Branch>> GetPagedList(
            PaginationRequest request,
            string? nameFilter,
            string? cityFilter,
            string? addressFilter,
            string? phoneFilter,
            BranchStatus?    statusFilter,
            CancellationToken cancellationToken = default)
        {
            IQueryable<Branch> query = _context.Branches.AsQueryable();

            if (!string.IsNullOrWhiteSpace(nameFilter))
            {
                string term = nameFilter.Trim();
                query = query.Where(b => b.Name.Contains(term));
            }

            if (!string.IsNullOrWhiteSpace(cityFilter))
            {
                string term = cityFilter.Trim();
                query = query.Where(b => b.City.Contains(term));
            }

            if (!string.IsNullOrWhiteSpace(addressFilter))
            {
                string term = addressFilter.Trim();
                query = query.Where(b => b.Address.Contains(term));
            }

            if (!string.IsNullOrWhiteSpace(phoneFilter))
            {
                string term = phoneFilter.Trim();
                query = query.Where(b =>b.Phone.Contains(term));
            }

            if (statusFilter.HasValue)
            {
                query = query.Where(b => b.Status == statusFilter.Value); 
            }

            query = query.OrderBy(b => b.Name);
            return await query.ToPagedListAsync(request, cancellationToken);
        }
    }
}
