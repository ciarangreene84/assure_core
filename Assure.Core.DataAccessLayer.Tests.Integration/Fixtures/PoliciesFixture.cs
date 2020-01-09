using Assure.Core.DataAccessLayer.Interfaces.DbContexts;
using Assure.Core.DataAccessLayer.Interfaces.Models;
using Assure.Core.DataAccessLayer.Tests.Integration.DbContexts;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assure.Core.DataAccessLayer.Tests.Integration.Fixtures
{
    public class PoliciesFixture : AbstractDbContextTests<IPoliciesDbContext, Policy>
    {
        public readonly IList<Policy> Items;

        public PoliciesFixture()
        {
            var getPoliciesTask = GetPoliciesAsync();
            getPoliciesTask.Wait();
            Items = getPoliciesTask.Result.ToList();
        }

        private async Task<IEnumerable<Policy>> GetPoliciesAsync()
        {
            _logger.LogInformation("GetPoliciesAsync...");
            using (var dbConnection = await DbConnectionFactory.OpenUserConnectionAsync(UserId))
            {
                return await _dbContext.GetAllAsync(dbConnection);
            }
        }
    }
}
