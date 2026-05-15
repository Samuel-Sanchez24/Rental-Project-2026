using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Rental_Project_2026.Persistence.Configurations
{
    internal interface IEntitytypeConfiguration<T>
    {
        void Configure(EntityTypeBuilder<RolePermissionConfig> builder);
    }
}