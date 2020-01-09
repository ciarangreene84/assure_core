using Assure.Core.DataAccessLayer.Interfaces.DbContexts;
using Assure.Core.RepositoryLayer.Implementation.Serialization;
using Assure.Core.RepositoryLayer.Interfaces.Models;
using Assure.Core.RepositoryLayer.Interfaces.Repositories;
using Boot4ServiceCollection.Attributes;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using DalModels = Assure.Core.DataAccessLayer.Interfaces.Models;

namespace Assure.Core.RepositoryLayer.Implementation.Repositories
{
    [AddScoped(typeof(IPoliciesRepository))]
    public sealed class PoliciesRepository : AbstractCoreRepository<IPoliciesDbContext, RepositoryLayer.Interfaces.Models.Policy, DataAccessLayer.Interfaces.Models.Policy>, IPoliciesRepository
    {
        public PoliciesRepository(ILogger<PoliciesRepository> logger, IObjectDocumentSerializer objectDocumentSerializer, IPoliciesDbContext dbContext) :
            base(logger, dbContext, objectDocumentSerializer)
        {

        }

        public async Task<IEnumerable<T>> GetCustomerPoliciesAsync<T>(IDbConnection dbConnection, int customerId) where T : Policy
        {
            _logger.LogInformation($"Getting policies for customer '{customerId}'...");
            var dalObject = await _dbContext.GetCustomerPoliciesAsync(dbConnection, customerId);
            return _objectDocumentSerializer.Deserialize<DalModels.Policy, T>(dalObject);
        }
    }
}
