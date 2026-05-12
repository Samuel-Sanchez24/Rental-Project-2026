using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rental_Project_2026.Domain.Entities;
using Rental_Project_2026.Persistence.Entities;

namespace Rental_Project_2026.Persistence.Seeding
{
    public class UsersSeeder 
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersSeeder(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task SeedAsync()
        {
            await CheckUserAsync();
        }

        public async Task CheckUserAsync()
        {
            string email = "adminuser@gmail.com";

            ApplicationUser? user = await _userManager.FindByEmailAsync(email);

            if(user is null)
            {
                user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true,
                    Firtsname = "Seed Admin",
                    Lastname = "Samuel"
                };

                IdentityResult userCreated = await _userManager.CreateAsync(user, "123456");
            }
        }

    }
}
