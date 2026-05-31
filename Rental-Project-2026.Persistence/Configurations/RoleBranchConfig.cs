using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rental_Project_2026.Domain.Entities.Account;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rental_Project_2026.Persistence.Configurations
{
    internal class RoleBranchConfig : IEntityTypeConfiguration<RoleBranch>
    {
        public void Configure(EntityTypeBuilder<RoleBranch> builder)
        {
            builder.HasKey(rb => new { rb.RoleId, rb.BranchId });

            builder.HasOne(rb => rb.Role)
                .WithMany(r => r.RoleBranches)
                .HasForeignKey(rb => rb.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(rb => rb.Branch)
                .WithMany(b => b.RoleBranches)
                .HasForeignKey(rb => rb.BranchId)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
