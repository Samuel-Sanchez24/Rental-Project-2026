using System;
using System.Collections.Generic;
using System.Text;
using Rental_Project_2026.Application.Contracts.Security;
using static Rental_Project_2026.Application.Contracts.Security.PermissionCodesCatalog;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Rental_Project_2026.Domain.Entities.Account;

namespace Rental_Project_2026.Persistence.Seeding
{
    public class PermissionsSeeder
    {
        private readonly DataContext _context;

        public PermissionsSeeder(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            foreach (PermissionSeed permission in PermissionCodesCatalog.All)
            {
                bool exists = await _context.Permissions.AnyAsync(p => p.Code == permission.Code);

                if (exists)
                {
                    continue;
                }

                await _context.AddAsync(new Permission(permission.Code, permission.Description, permission.Module));
                await _context.SaveChangesAsync();
            }
        }
    }
}
