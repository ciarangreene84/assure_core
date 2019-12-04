using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Assure.Core.IdentityLayer.Implementation.DbContexts
{
    public class AssureIdentityDbContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public AssureIdentityDbContext(DbContextOptions<AssureIdentityDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("Identity");
            base.OnModelCreating(builder);

            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims").Property(e => e.Id).HasColumnName("RoleClaimId");
            builder.Entity<IdentityRole<Guid>>().ToTable("Roles").Property(e => e.Id).HasColumnName("RoleId");
            builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims").Property(e => e.Id).HasColumnName("UserClaimId");
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins");
            builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles");
            builder.Entity<IdentityUser<Guid>>().ToTable("Users").Property(e => e.Id).HasColumnName("UserId");
            builder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens");
        }

#pragma warning disable EF1000
        public Task<int> AddDatabaseUser(IdentityUser<Guid> user)
        {
            var createUserCommand = $"CREATE USER [{user.Id}] WITHOUT LOGIN";
            return Database.ExecuteSqlCommandAsync(createUserCommand);
        }
#pragma warning restore EF1000

#pragma warning disable EF1000
        public Task DropDatabaseUser(IdentityUser<Guid> user)
        {
            var dropUserCommand = $"DROP USER [{user.Id}]";
            return Database.ExecuteSqlCommandAsync(dropUserCommand);
        }
#pragma warning restore EF1000
    }
}
