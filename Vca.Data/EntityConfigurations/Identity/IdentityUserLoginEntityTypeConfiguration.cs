using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Vca.Data.EntityConfigurations.Identity
{
    class IdentityUserLoginEntityTypeConfiguration : IEntityTypeConfiguration<IdentityUserLogin<long>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserLogin<long>> builder)
        {
            builder.ToTable("UserLogins");

            builder.HasKey(x => x.UserId);

            builder.Property(p => p.LoginProvider)
                .HasMaxLength(50);

            builder.Property(p => p.ProviderDisplayName)
                .HasMaxLength(100);

            builder.Property(p => p.ProviderKey)
                .HasMaxLength(500);
        }
    }
}
