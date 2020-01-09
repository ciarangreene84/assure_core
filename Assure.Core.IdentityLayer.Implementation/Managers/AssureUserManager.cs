using Assure.Core.IdentityLayer.Implementation.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assure.Core.IdentityLayer.Implementation.Managers
{
    public class AssureUserManager : UserManager<IdentityUser<Guid>>
    {
        private readonly AssureIdentityDbContext _dbContext;

        public AssureUserManager(IUserStore<IdentityUser<Guid>> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<IdentityUser<Guid>> passwordHasher, IEnumerable<IUserValidator<IdentityUser<Guid>>> userValidators, IEnumerable<IPasswordValidator<IdentityUser<Guid>>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<IdentityUser<Guid>>> logger, AssureIdentityDbContext dbContext) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _dbContext = dbContext;
        }

        public override async Task<IdentityResult> CreateAsync(IdentityUser<Guid> user)
        {
            var result = await base.CreateAsync(user);
            if (result.Succeeded)
            {
                await _dbContext.AddDatabaseUser(user);
            }
            return result;
        }

        public override async Task<IdentityResult> DeleteAsync(IdentityUser<Guid> user)
        {
            var result = await base.DeleteAsync(user);
            if (result.Succeeded)
            {
                await _dbContext.DropDatabaseUser(user);
            }
            return result;
        }
    }
}
