using Microsoft.EntityFrameworkCore;
using Rental_Project_2026.Application.Contracts.Pagination;
using Rental_Project_2026.Application.Contracts.Repositories;
using Rental_Project_2026.Domain.Entities;
using Rental_Project_2026.Persistence.Extensions;

namespace Rental_Project_2026.Persistence.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DataContext _context;

        public UsersRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<User> CreateAsync(User user)
        {
            await _context.SystemUsers.AddAsync(user);
            return user;
        }

        public Task UpdateAsync(User user)
        {
            _context.SystemUsers.Update(user);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(User user)
        {
            _context.SystemUsers.Remove(user);
            return Task.CompletedTask;
        }

        public async Task<User?> GetByIdAsync(string id)
        {
            return await _context.SystemUsers
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.SystemUsers
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<User>> GetListAsync()
        {
            return await _context.SystemUsers
                .ToListAsync();
        }

        public async Task<PaginationResponse<User>> GetPagedList(
            PaginationRequest request,
            string? nameFilter,
            string? emailFilter,
            UserRole? roleFilter,
            UserStatus? statusFilter,
            CancellationToken cancellationToken = default)
        {
            IQueryable<User> query = _context.SystemUsers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(nameFilter))
            {
                query = query.Where(u =>
                    u.FirstName.Contains(nameFilter) ||
                    u.LastName.Contains(nameFilter) ||
                    u.UserName.Contains(nameFilter));
            }

            if (!string.IsNullOrWhiteSpace(emailFilter))
            {
                query = query.Where(u => u.Email.Contains(emailFilter));
            }

            if (roleFilter.HasValue)
            {
                query = query.Where(u => u.Role == roleFilter.Value);
            }

            if (statusFilter.HasValue)
            {
                query = query.Where(u => u.Status == statusFilter.Value);
            }

            query = query.OrderBy(u => u.FirstName);

            return await query.ToPagedListAsync(request, cancellationToken);
        }
    }
}
