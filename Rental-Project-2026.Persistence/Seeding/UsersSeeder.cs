using Microsoft.EntityFrameworkCore;
using Rental_Project_2026.Domain.Entities;

namespace Rental_Project_2026.Persistence.Seeding
{
    internal class UsersSeeder : ISeedable
    {
        private readonly DataContext _context;

        public UsersSeeder(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            List<User> usersToSeed = new List<User>
            {
                new User("Administrador", "admin@rentalproject.com", "Password123", "3001234567", UserRole.Admin),
                new User("Empleado Central", "empleado@rentalproject.com", "Password123", "3001234568", UserRole.Employee),
                new User("Cliente", "cliente@rentalproject.com", "Password123", "3001234569", UserRole.Customer)
            };

            foreach (User user in usersToSeed)
            {
                bool exists = await _context.Users.AnyAsync(u => u.Email == user.Email);
                if (!exists)
                {
                    await _context.Users.AddAsync(user);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
