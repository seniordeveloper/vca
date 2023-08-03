using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Vca.Data.Entities;
using Vca.Data.Entities.Identity;
using Vca.Data.EntityConfigurations;
using Vca.Data.EntityConfigurations.Identity;

namespace Vca.Data
{
    public class VcaDbContext : IdentityDbContext<UserEntity, RoleEntity, long>
    {
        public VcaDbContext(DbContextOptions<VcaDbContext> options) : base(options) { }

        public override DbSet<UserEntity> Users { get; set; }
        public override DbSet<RoleEntity> Roles { get; set; }
        public DbSet<UserContactEntity> UserContacts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) 
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserEntityTypeConfiguration())
                   .ApplyConfiguration(new RoleEntityTypeConfiguration())
                   .ApplyConfiguration(new IdentityRoleClaimEntityTypeConfiguration())
                   .ApplyConfiguration(new IdentityUserClaimEntityTypeConfiguration())
                   .ApplyConfiguration(new IdentityUserLoginEntityTypeConfiguration())
                   .ApplyConfiguration(new IdentityUserRoleEntityTypeConfiguration())
                   .ApplyConfiguration(new IdentityUserTokenEntityTypeConfiguration())
                   .ApplyConfiguration(new UserContactEntityTypeConfiguration());
        }
    }
}
