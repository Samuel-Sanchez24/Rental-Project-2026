using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rental_Project_2026.Domain.Account;
using Rental_Project_2026.Domain.Entities;
using Rental_Project_2026.Domain.Entities.Account;
using Rental_Project_2026.Domain.Entities.Branches;
using Rental_Project_2026.Persistence.Entities;


namespace Rental_Project_2026.Persistence
{
    public class DataContext : IdentityUserContext<ApplicationUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<User> DomainUsers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Payment> Payments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Vehicle>()
                .Property(v => v.DailyPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Reservation>()
                .Property(r => r.DailyPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Reservation>()
                .Property(r => r.TotalPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasPrecision(18, 2);
        }
    }
}
