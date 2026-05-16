using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rental_Project_2026.Domain.Entities;

namespace Rental_Project_2026.Persistence.Configurations
{
    public class VehicleConfig : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.Property(v => v.Plate)
       .HasMaxLength(10)
       .IsRequired();

            builder.HasIndex(v => v.Plate)
                   .IsUnique();

            builder.Property(v => v.Brand)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(v => v.Model)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(v => v.Year)
                   .IsRequired();

            builder.Property(v => v.Color)
                   .HasMaxLength(30)
                   .IsRequired();

            builder.Property(v => v.DailyPrice)
                   .HasPrecision(18, 2)
                   .IsRequired();

            builder.Property(v => v.Status)
                   .IsRequired();

            builder.Property(v => v.ImageUrl)
                   .HasMaxLength(512);

            builder.Property(v => v.BranchId)
                   .IsRequired();

            builder.HasOne(v => v.Branch)
                   .WithMany()
                   .HasForeignKey(v => v.BranchId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
