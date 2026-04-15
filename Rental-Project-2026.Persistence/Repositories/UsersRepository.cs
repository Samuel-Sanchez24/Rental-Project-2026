using Rental_Project_2026.Application.Contracts.Repositories;
using Rental_Project_2026.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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
    }
}
