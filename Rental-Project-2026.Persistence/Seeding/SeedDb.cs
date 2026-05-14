using Microsoft.AspNetCore.Identity;
using Rental_Project_2026.Persistence.Entities;

namespace Rental_Project_2026.Persistence.Seeding
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SeedDb(DataContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task SeedAsync()
        {
            await new BranchesSeeder(_context).SeedAsync();
            await new PermissionsSeeder(_context).SeedAsync();
            await new UsersSeeder(_userManager, _context ).SeedAsync();
        }
    }
}