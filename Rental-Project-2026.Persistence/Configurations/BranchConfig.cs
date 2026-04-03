using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Rental_Project_2026.Persistence.Configurations
{
    public class BranchConfig : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.Property(b => b.Name)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(b => b.City)
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(b => b.Address)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(b => b.Phone)
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(b => b.Status)
                   .IsRequired();
        }
    }
}
