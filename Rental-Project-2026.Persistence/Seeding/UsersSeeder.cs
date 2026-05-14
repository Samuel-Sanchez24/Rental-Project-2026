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
            await SeedUserAsync();
        }

        private async Task SeedUserAsync()
        {
            await CheckUserAsync("adminuser@gmail.com", "Seed Admin", "", RolesCatalog.ADMIN);
            await CheckUserAsync("basicuser@gmail.com", "Jhon", "Doe", RolesCatalog.USER);
        }

        private async Task SeedRolesAsync()
        {
            await CheckRoleAsync(RolesCatalog.ADMIN, PermissionCodesCatalog.All.Select(s => s.Code).ToList());
            await CheckRoleAsync(RolesCatalog.CONTENT_EDITOR, new List<string>
            {
                PermissionCodesCatalog.SHOW_BLOGS,
                PermissionCodesCatalog.CREATE_BLOGS,
                PermissionCodesCatalog.EDIT_BLOGS,
                PermissionCodesCatalog.DELETE_BLOGS,

                PermissionCodesCatalog.SHOW_SECTIONS,
                PermissionCodesCatalog.CREATE_SECTIONS,
            });

            await CheckRoleAsync(RolesCatalog.USER, new List<string>
            {
                PermissionCodesCatalog.SHOW_BLOGS,

                PermissionCodesCatalog.SHOW_SECTIONS,

            }); 
        }    

        public async Task CheckUserAsync(string email, string firstname, string LastName, string RoleName)
        {
            Role role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == RoleName);

            ApplicationUser? user = await _userManager.FindByEmailAsync(email);

            if (user is null)
            {
                user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true,
                    Firtsname = firstname,
                    Lastname = LastName,
                    RoleId = role.Id
                };

                IdentityResult userCreated = await _userManager.CreateAsync(user, "123456");
            }
        }

        private async Task CheckRoleAsync(string roleName, IReadOnlyList<string> permissionCodes)
        {
            Role? role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
            if (role is null)
            {
                role = new Role(roleName);
                await _context.Roles.AddAsync(role);
                await _context.SaveChangesAsync();
            }

            List<Guid> permissionIds = await _context.Permissions.Where(p => permissionCodes.Contains (p.Code))
                                                                 .Select(p => p.Id)
                                                                 .ToListAsync();

            List<Guid> existingPermissionIds = await _context.RolePermissions.Where(rp => rp.RoleId == role.Id)
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
