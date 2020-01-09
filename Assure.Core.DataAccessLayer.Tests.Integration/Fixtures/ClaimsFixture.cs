using Assure.Core.DataAccessLayer.Interfaces.DbContexts;
using Assure.Core.DataAccessLayer.Interfaces.Models;
using Assure.Core.DataAccessLayer.Tests.Integration.DbContexts;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assure.Core.DataAccessLayer.Tests.Integration.Fixtures
{
    public class ClaimsFixture : AbstractDbContextTests<IClaimsDbContext, Claim>
    {
        public readonly IList<Claim> Items;

        public ClaimsFixture()
        {
            var getClaimsTask = GetClaimsAsync();
            getClaimsTask.Wait();
            Items = getClaimsTask.Result.ToList();
        }

        private async Task<IEnumerable<Claim>> GetClaimsAsync()
        {
            _logger.LogInformation("GetClaimsAsync...");
            using (var dbConnection = await DbConnectionFactory.OpenUserConnectionAsync(UserId))
            {
                return await _dbContext.GetAllAsync(dbConnection);
            }
        }
    }
}
