using Assure.Core.IdentityLayer.Implementation.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using GenFu;
using Xunit;


namespace Assure.Core.IdentityLayer.Tests.Integration.DbContexts
{
    public class SecurityDbContextTests
    {
        private readonly AssureIdentityDbContext _dbContext;
        private readonly UserManager<IdentityUser<Guid>> _userManager;

        public SecurityDbContextTests()
        {
            var services = new ServiceCollection();
            services.AddOptions();
            services.AddLogging();

            services.AddAssureCoreIdentityLayer("Data Source=localhost;Initial Catalog=AssureCore;User Id=sa;Password=MySuperStrongPassword1!;");

            var serviceProvider = services.BuildServiceProvider();
            _dbContext = serviceProvider.GetService<AssureIdentityDbContext>();
            _userManager = serviceProvider.GetService<UserManager<IdentityUser<Guid>>>();
        }

        [Fact]
        public async void CreateUser()
        {
            var user = A.New<IdentityUser<Guid>>();
            await _userManager.CreateAsync(user, "A55uR3c0R3$");
        }

        [Fact]
        public void RoleClaims()
        {
            var result = _dbContext.RoleClaims.ToList();
            Assert.NotNull(result);
        }
        [Fact]
        public void Roles()
        {
            var result = _dbContext.Roles.ToList();
            Assert.NotNull(result);
        }
        [Fact]
        public void UserClaims()
        {
            var result = _dbContext.UserClaims.ToList();
            Assert.NotNull(result);
        }
        [Fact]
        public void UserLogins()
        {
            var result = _dbContext.UserLogins.ToList();
            Assert.NotNull(result);
        }
        [Fact]
        public void UserRoles()
        {
            var result = _dbContext.UserRoles.ToList();
            Assert.NotNull(result);
        }
        [Fact]
        public void Users()
        {
            var result = _dbContext.Users.ToList();
            Assert.NotNull(result);
        }
        [Fact]
        public void UserTokens()
        {
            var result = _dbContext.UserTokens.ToList();
            Assert.NotNull(result);
        }
    }
}
