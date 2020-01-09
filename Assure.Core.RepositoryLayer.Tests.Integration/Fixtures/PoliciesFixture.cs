using Assure.Core.RepositoryLayer.Interfaces.Repositories;
using Assure.Core.RepositoryLayer.Tests.Integration.Repositories;
using Assure.Core.RepositoryLayer.Tests.Integration.TestModels;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assure.Core.RepositoryLayer.Tests.Integration.Fixtures
{
    public class PoliciesFixture : AbstractCoreRepositoryTests<IPoliciesRepository, TestPolicy>
    {
        public readonly IList<TestPolicy> Items;
        public PoliciesFixture()
        {
            var getPoliciesTask = GetPoliciesAsync();
            getPoliciesTask.Wait();
            Items = getPoliciesTask.Result.ToList();
        }
        
        private async Task<IEnumerable<TestPolicy>> GetPoliciesAsync()
        {
            _logger.LogInformation("GetPoliciesAsync...");
            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                return await _repository.GetAsync<TestPolicy>(dbConnection);
            }
        }
    }
}
