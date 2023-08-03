using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Vca.Data.EntityConfigurations.Identity
{
    class IdentityUserTokenEntityTypeConfiguration : IEntityTypeConfiguration<IdentityUserToken<long>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserToken<long>> builder)
        {
            builder.ToTable("UserTokens");

            builder.Property(p => p.Name)
                .HasMaxLength(200);

            builder.Property(p => p.Value)
                .HasMaxLength(1000);

            builder.Property(p => p.LoginProvider)
                .HasMaxLength(50);
        }
    }
}
