using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Vca.Data.EntityConfigurations.Identity
{
    class IdentityRoleClaimEntityTypeConfiguration : IEntityTypeConfiguration<IdentityRoleClaim<long>>
    {
        public void Configure(EntityTypeBuilder<IdentityRoleClaim<long>> builder)
        {
            builder.ToTable("RoleClaims");

            builder.HasKey(x => x.Id);

            builder.Property(p => p.ClaimType)
                .HasMaxLength(50);

            builder.Property(p => p.ClaimValue)
                .HasMaxLength(100);
        }
    }
}
