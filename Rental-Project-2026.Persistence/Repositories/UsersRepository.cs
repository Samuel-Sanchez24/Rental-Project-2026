using Microsoft.EntityFrameworkCore;
using Rental_Project_2026.Application.Contracts.Pagination;
using Rental_Project_2026.Application.Contracts.Repositories;
using Rental_Project_2026.Domain.Entities;
using Rental_Project_2026.Persistence.Extensions;

namespace Rental_Project_2026.Persistence.Repositories
{
    public class UsersRepository : Repository<User>, IUsersRepository
    {
        private readonly DataContext _context;

        public UsersRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<PaginationResponse<User>> GetPagedList(
            PaginationRequest request,
            string? nameFilter,
            string? emailFilter,
            UserRole? roleFilter,
            UserStatus? statusFilter,
            CancellationToken cancellationToken = default)
        {
            IQueryable<User> query = _context.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(nameFilter))
            {
                string term = nameFilter.Trim();
                query = query.Where(u => u.Name.Contains(term));
            }

            if (!string.IsNullOrWhiteSpace(emailFilter))
            {
                string term = emailFilter.Trim();
                query = query.Where(u => u.Email.Contains(term));
            }

            if (roleFilter.HasValue)
            {
                query = query.Where(u => u.Role == roleFilter.Value);
            }

            if (statusFilter.HasValue)
            {
                query = query.Where(u => u.Status == statusFilter.Value);
            }

            query = query.OrderBy(u => u.Name);

            return await query.ToPagedListAsync(request, cancellationToken);
        }
    }
}
