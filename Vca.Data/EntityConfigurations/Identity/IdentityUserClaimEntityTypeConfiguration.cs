using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Vca.Data.EntityConfigurations.Identity
{
    class IdentityUserClaimEntityTypeConfiguration : IEntityTypeConfiguration<IdentityUserClaim<long>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserClaim<long>> builder)
        {
            builder.ToTable("UserClaims");

            builder.HasKey(x => x.Id);

            builder.Property(p => p.ClaimType)
               .HasMaxLength(50);

            builder.Property(p => p.ClaimValue)
                .HasMaxLength(100);
        }
    }
}
