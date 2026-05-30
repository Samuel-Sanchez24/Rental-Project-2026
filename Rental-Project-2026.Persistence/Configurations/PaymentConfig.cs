using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rental_Project_2026.Domain.Entities;

namespace Rental_Project_2026.Persistence.Configurations
{
    public class PaymentConfig : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Amount)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(p => p.Status)
                .IsRequired();

            builder.Property(p => p.Provider)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.ProviderReference)
                .HasMaxLength(120)
                .IsRequired(false);

            builder.Property(p => p.PaymentUrl)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(p => p.CreatedAt)
                .IsRequired();

            builder.Property(p => p.UpdatedAt)
                .IsRequired();

            builder.Property(p => p.PaidAt)
                .IsRequired(false);

            builder.HasOne(p => p.Reservation)
                .WithMany()
                .HasForeignKey(p => p.ReservationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
