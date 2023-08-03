using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using Vca.Data;

namespace Vca.Tests.FakeUtils
{
    [ExcludeFromCodeCoverage]
    public static class DbHelper
    {
        /// <summary>
        /// Returns database context options for unit test.
        /// </summary>
        public static DbContextOptions<VcaDbContext> GetDbContextOptions()
        {
            var dbName = $"{nameof(Vca)}InMemoryDb";
            var inMemoryDbOptions = new DbContextOptionsBuilder<VcaDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;

            return inMemoryDbOptions;
        }
    }
}
