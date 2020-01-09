using Assure.Core.RepositoryLayer.Interfaces.Repositories;
using Assure.Core.RepositoryLayer.Tests.Integration.Repositories;
using Assure.Core.RepositoryLayer.Tests.Integration.TestModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assure.Core.RepositoryLayer.Tests.Integration.Fixtures
{
    public class QuotesFixture : AbstractCoreRepositoryTests<IQuotesRepository, TestQuote>
    {
        public readonly IList<TestQuote> Items;

        public QuotesFixture()
        {
            var getPoliciesTask = GetPoliciesAsync();
            getPoliciesTask.Wait();
            Items = getPoliciesTask.Result.ToList();
        }

        private async Task<IEnumerable<TestQuote>> GetPoliciesAsync()
        {
            _logger.LogInformation("GetPoliciesAsync...");
            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                return await _repository.GetAsync<TestQuote>(dbConnection);
            }
        }
    }
}
