using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using Vca.Data.Entities.Identity;

namespace Vca.Data.Extensions
{
    [ExcludeFromCodeCoverage]
    /// <summary>
    /// Contains extensions methods to query <see cref="VcaDbContext"/>.
    /// </summary>
    public static partial class VcaDbContextExtensions
    {
        public static Task<UserEntity> FindByEmailAsync(this DbSet<UserEntity> users, string email, CancellationToken cancellationToken = default(CancellationToken))
        {
            return users.SingleOrDefaultAsync(w => w.Email == email, cancellationToken);
        }

        public static Task<UserEntity> FindByIdAsync(this DbSet<UserEntity> users, int userId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return users.SingleOrDefaultAsync(w => w.Id == userId, cancellationToken);
        }
    }
}
