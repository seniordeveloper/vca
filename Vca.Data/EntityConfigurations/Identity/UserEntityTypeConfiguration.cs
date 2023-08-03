using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vca.Data.Entities.Identity;

namespace Vca.Data.EntityConfigurations.Identity
{
    class UserEntityTypeConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable(nameof(VcaDbContext.Users));

            builder.HasKey(p => p.Id);

            builder.HasIndex(p => p.UserName).IsUnique(true);

            builder.HasIndex(p => p.Email).IsUnique(true);

            builder.HasIndex(p => p.PhoneNumber).IsUnique(true);

            builder.Property(u => u.UserName)
              .HasMaxLength(UserEntity.UserNameMaxLength);

            builder
                .Property(u => u.NormalizedUserName)
                .HasMaxLength(UserEntity.NormalizedUserNameMaxLength);

            builder
                .Property(u => u.PhoneNumber)
                .HasMaxLength(UserEntity.PhoneNumberMaxLength);

            builder
                .Property(u => u.Email)
                .HasMaxLength(UserEntity.EmailMaxLength)
                .IsRequired();

            builder
                .Property(u => u.NormalizedEmail)
                .HasMaxLength(UserEntity.NormalizedEmailLength);

            builder
               .Property(u => u.FirstName)
               .IsRequired()
               .HasMaxLength(UserEntity.FirstNameMaxLength);

            builder
               .Property(u => u.LastName)
               .IsRequired()
               .HasMaxLength(UserEntity.LastNameMaxLength);
        }
    }
}
