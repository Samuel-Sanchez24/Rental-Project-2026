using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rental_Project_2026.Application.Contracts.Security;
using Rental_Project_2026.Domain.Entities;
using Rental_Project_2026.Domain.Entities.Account;
using Rental_Project_2026.Persistence.Entities;


namespace Rental_Project_2026.Persistence.Seeding
{
    public class UsersSeeder
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly DataContext _context;

        public UsersSeeder(UserManager<ApplicationUser> userManager, DataContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task SeedAsync()   
        {
            await SeedRolesAsync();
            await SeedUserAsync();
        }

        private async Task SeedUserAsync()
        {
            await CheckUsersAsync("adminuser@gmail.com", "Seed", "Admin", RolesCatalog.ADMIN);
            await CheckUsersAsync("basicuser@gmail.com", "Jhon", "Doe", RolesCatalog.CUSTOMER);
            await CheckUsersAsync("employeeuser@gmail.com", "Jane", "Smith", RolesCatalog.EMPLOYEE);
        }

        private async Task SeedRolesAsync()
        {
            await CheckRolesAsync(RolesCatalog.ADMIN, PermissionCodesCatalog.All.Select(s => s.Code).ToList());

            await CheckRolesAsync(RolesCatalog.EMPLOYEE, new List<string>
            {
                PermissionCodesCatalog.SHOW_VEHICLES,
                PermissionCodesCatalog.EDIT_VEHICLES,

                PermissionCodesCatalog.SHOW_BRANCHES,

                PermissionCodesCatalog.CREATE_USERS,
                PermissionCodesCatalog.EDIT_USERS,
                PermissionCodesCatalog.SHOW_USERS,

                PermissionCodesCatalog.SHOW_RESERVATION,
                PermissionCodesCatalog.CREATE_RESERVATIONS,
                PermissionCodesCatalog.EDIT_RESERVATIONS,
                PermissionCodesCatalog.CANCEL_RESERVATIONS,


            });

            await CheckRolesAsync(RolesCatalog.CUSTOMER, new List<string>
            {
                PermissionCodesCatalog.SHOW_BRANCHES,

                PermissionCodesCatalog.SHOW_VEHICLES,

                PermissionCodesCatalog.SHOW_RESERVATION,
                PermissionCodesCatalog.CREATE_RESERVATIONS,
                PermissionCodesCatalog.CANCEL_RESERVATIONS

            }); 
        }

        private async Task CheckUsersAsync(string email, string firstName, string lastName, string roleName)
        {
            Role role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);

            ApplicationUser? user = await _userManager.FindByEmailAsync(email);

            if (user is null)
            {
                user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true,
                    FirstName = firstName,
                    LastName = lastName,
                    RoleId = role.Id
                };

                await _userManager.CreateAsync(user, "1234");
            }
        }

        private async Task CheckRolesAsync(string roleName, List<string> permissionCodes)
        {
            Role? role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
            if (role is null)
            {
                role = new Role(roleName);
                await _context.Roles.AddAsync(role);
                await _context.SaveChangesAsync();
            }

            List<Guid> permissionIds = await _context.Permissions.Where(p => permissionCodes.Contains(p.Code))
                                                                 .Select(p => p.Id)
                                                                 .ToListAsync();

            List<Guid> existingPermissionIds = await _context.RolePermissions
                                                             .Where(rp => rp.RoleId == role.Id)
                                                             .Select(rp => rp.PermissionId)
                                                             .ToListAsync();

            List<Guid> toAdd = permissionIds.Except(existingPermissionIds)
                                            .ToList();

            foreach (Guid permissionId in toAdd)
            {
                RolePermission rolePermission = new RolePermission(role.Id, permissionId);
                await _context.RolePermissions.AddAsync(rolePermission);
            }

            await _context.SaveChangesAsync();
        }
    }
}
