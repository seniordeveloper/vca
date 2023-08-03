using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vca.Data.Entities;

namespace Vca.Data.EntityConfigurations
{
    class UserContactEntityTypeConfiguration : IEntityTypeConfiguration<UserContactEntity>
    {
        public void Configure(EntityTypeBuilder<UserContactEntity> builder) 
        {
            builder.ToTable(nameof(VcaDbContext.UserContacts));

            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(x => x.UserId);

            builder.Property(u => u.Name)
              .IsRequired()
              .HasMaxLength(UserContactEntity.NameMaxLength);

            builder.Property(u => u.PhoneNumber)
              .IsRequired()
              .HasMaxLength(UserContactEntity.PhoneNumberMaxLength);
        }
    }
}
