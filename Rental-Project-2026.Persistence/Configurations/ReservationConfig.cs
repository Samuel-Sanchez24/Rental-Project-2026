using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rental_Project_2026.Domain.Entities;
using Rental_Project_2026.Persistence.Entities;

namespace Rental_Project_2026.Persistence.Configurations
{
    public class ReservationConfig : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.ToTable("Reservations");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.RentDate)
                .IsRequired();

            builder.Property(r => r.ReturnDate)
                .IsRequired();

            builder.Property(r => r.Days)
                .IsRequired();

            builder.Property(r => r.DailyPrice)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(r => r.TotalPrice)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(r => r.Status)
                .IsRequired();

            builder.Property(r => r.CreatedAt)
                .IsRequired();

            // Customer information
            builder.Property(r => r.CustomerFullName)
                .HasMaxLength(120)
                .IsRequired();

            builder.Property(r => r.DocumentNumber)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(r => r.PhoneNumber)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(r => r.Email)
                .HasMaxLength(120)
                .IsRequired();

            builder.Property(r => r.BirthDate)
                .IsRequired();

            // Driver license information
            builder.Property(r => r.DriverLicenseCategories)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(r => r.DriverLicenseExpirationDate)
                .IsRequired();

            // Special assistance
            builder.Property(r => r.RequiresSpecialAssistance)
                .IsRequired();

            builder.Property(r => r.AssistanceNotes)
                .HasMaxLength(300)
                .IsRequired(false);

            builder.Property(r => r.UserId)
                .HasMaxLength(450)
                .IsRequired();

            builder.HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Vehicle)
                .WithMany()
                .HasForeignKey(r => r.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Branch)
                .WithMany()
                .HasForeignKey(r => r.BranchId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}