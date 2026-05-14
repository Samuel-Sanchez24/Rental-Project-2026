using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rental_Project_2026.Domain.Entities.Account;

namespace Rental_Project_2026.Persistence.Configurations
{
    public class PermissionConfig : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Code)
                .IsRequired()
                .HasMaxLength(64);

            builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(p => p.Module)
                .IsRequired()
                .HasMaxLength(32);

            builder.HasIndex(p => p.Code)
                .IsUnique();
        }
    }
}
