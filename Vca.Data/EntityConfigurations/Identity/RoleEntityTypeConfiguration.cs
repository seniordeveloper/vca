using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vca.Data;
using Vca.Data.Entities.Identity;

namespace Vca.Data.EntityConfigurations.Identity
{
    class RoleEntityTypeConfiguration : IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            builder.ToTable(nameof(VcaDbContext.Roles));

            builder
                .Property(p => p.Name)
                .HasMaxLength(50);

            builder.Property(p => p.NormalizedName)
                .HasMaxLength(50);

            builder.Property(p => p.ConcurrencyStamp)
                .HasMaxLength(200);
        }
    }
}
