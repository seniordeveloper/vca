using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Vca.Abstractions.Services;
using Vca.Abstractions.Services.Identity;
using Vca.Core.Services;
using Vca.Core.Services.Identity;
using Vca.Data;
using Vca.Data.Entities.Identity;

namespace Vca.Extensions
{
    /// <summary>
    /// Contains extension methods to <see cref="IServiceCollection"/> for configuring services.
    /// </summary>
    public static class ServiceConfiguraionExtensions
    {
        public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<VcaDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("VcaDbConnectionString")));

            return services;
        }

        public static IServiceCollection AddIdentity(this IServiceCollection services) 
        {
            services.AddIdentity<UserEntity, RoleEntity>(o =>
            {
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 1;
            }).AddEntityFrameworkStores<VcaDbContext>()
            .AddDefaultTokenProviders()
            .AddUserStore<UserStore<UserEntity, RoleEntity, VcaDbContext, long>>()
            .AddRoleStore<RoleStore<RoleEntity, VcaDbContext, long>>()
            .AddRoleManager<RoleManager<RoleEntity>>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services) 
        {
            services.AddTransient<IContactManager, ContactManager>();
            services.AddTransient<IAccountManager, AccountManager>();

            services.AddSingleton<IErrorDescriber, ErrorDescriber>();

            return services;
        }
    }
}
