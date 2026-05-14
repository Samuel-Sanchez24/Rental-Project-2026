using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rental_Project_2026.Domain.Entities.Account;

namespace Rental_Project_2026.Persistence.Configurations
{
    public class RolePermissionConfig : IEntityTypeConfiguration<RolePermission>
    {

        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.HasKey(rp => new { rp.RoleId, rp.PermissionId });

            builder.HasOne(rp => rp.Role)
                   .WithMany(r => r.RolePermissions)
                   .HasForeignKey(rp => rp.RoleId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(rp => rp.Permission)
                   .WithMany(p => p.RolePermissions)
                   .HasForeignKey(rp => rp.PermissionId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}