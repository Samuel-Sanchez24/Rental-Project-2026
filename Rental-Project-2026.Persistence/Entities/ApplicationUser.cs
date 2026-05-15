using Microsoft.AspNetCore.Identity;
using Rental_Project_2026.Domain.Entities.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Persistence.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Firtsname { get; set; }
        public string Lastname { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
    }
}
