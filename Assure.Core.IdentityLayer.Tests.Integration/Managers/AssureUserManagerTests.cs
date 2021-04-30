using Assure.Core.IdentityLayer.Implementation.Managers;
using GenFu;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text;
using Xunit;

namespace Assure.Core.IdentityLayer.Tests.Integration.Managers
{
    public class AssureUserManagerTests
    {
        private readonly AssureUserManager _userManager;
        private readonly SignInManager<IdentityUser<Guid>> _signInManager;

        public AssureUserManagerTests()
        {
            var services = new ServiceCollection();
            services.AddOptions();
            services.AddLogging();

            services.AddAuthentication();
            services.AddAssureCoreIdentityLayer("Data Source=localhost;Initial Catalog=AssureCore;User Id=sa;Password=MySuperStrongPassword1!;").AddSignInManager();

            var serviceProvider = services.BuildServiceProvider();
            _userManager = serviceProvider.GetService<AssureUserManager>();
            _signInManager = serviceProvider.GetService<SignInManager<IdentityUser<Guid>>>();
        }

        [Fact]
        public async void CheckPasswordSignInAsync()
        {
            var sysAdminUser = await _userManager.FindByEmailAsync("sysadmin@assurecore.fake");
            Assert.NotNull(sysAdminUser);

            var signInResult = await _signInManager.CheckPasswordSignInAsync(sysAdminUser, "A55uR3c0R3!", false);
            Assert.True(signInResult.Succeeded);
        }

        [Fact]
        public async void CreateAsync()
        {
            var user = A.New<IdentityUser<Guid>>();
            var result = await _userManager.CreateAsync(user, "A55uR3c0R3$");
            Assert.True(result.Succeeded);

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, "A55uR3c0R3$", false);
            Assert.True(signInResult.Succeeded);
        }

        [Fact]
        public async void DeleteAsync()
        {
            var user = A.New<IdentityUser<Guid>>();
            var createResult = await _userManager.CreateAsync(user, "A55uR3c0R3$");
            Assert.True(createResult.Succeeded);

            var deleteResult = await _userManager.DeleteAsync(user);
            Assert.True(deleteResult.Succeeded);
        }
    }
}

