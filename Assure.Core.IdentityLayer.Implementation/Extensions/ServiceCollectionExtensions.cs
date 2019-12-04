using Assure.Core.IdentityLayer.Implementation.DbContexts;
using Assure.Core.IdentityLayer.Implementation.Managers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IdentityBuilder AddAssureCoreIdentityLayer(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AssureIdentityDbContext>(options => options.UseSqlServer(connectionString));
            return services.AddIdentityCore<IdentityUser<Guid>>()
                            .AddRoles<IdentityRole<Guid>>()
                            .AddEntityFrameworkStores<AssureIdentityDbContext>()
                            .AddUserManager<AssureUserManager>();
        }
    }
}
