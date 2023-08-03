using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Vca.Data
{
    public class DesignTimeVcaDbContextFactory : IDesignTimeDbContextFactory<VcaDbContext>
    {
        public VcaDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<VcaDbContext>();
            var connectionString = configuration.GetConnectionString("VcaDbConnectionString");
            builder.UseSqlServer(connectionString);

            return new VcaDbContext(builder.Options);
        }
    }
}
