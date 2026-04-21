using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rental_Project_2026.Domain.Entities;

namespace Rental_Project_2026.Persistence.Configurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.Property(u => u.Name)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(u => u.Email)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(u => u.PasswordHash)
                   .HasMaxLength(255)
                   .IsRequired();

            builder.Property(u => u.Phone)
                   .HasMaxLength(12)
                   .IsRequired();

            builder.Property(u => u.Role)
                   .IsRequired();

            builder.Property(u => u.Status)
                   .IsRequired();

            builder.HasIndex(u => u.Email)
                   .IsUnique();
        }
    }
}
