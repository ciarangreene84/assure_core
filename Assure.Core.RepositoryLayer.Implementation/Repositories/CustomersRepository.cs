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
    [AddScoped(typeof(ICustomersRepository))]
    public sealed class CustomersRepository : AbstractCoreRepository<ICustomersDbContext, RepositoryLayer.Interfaces.Models.Customer, DataAccessLayer.Interfaces.Models.Customer>, ICustomersRepository
    {
        public CustomersRepository(ILogger<CustomersRepository> logger, IObjectDocumentSerializer objectDocumentSerializer, ICustomersDbContext dbContext) :
            base(logger, dbContext, objectDocumentSerializer)
        {

        }
        
        public async Task<IEnumerable<T>> GetPolicyCustomersAsync<T>(IDbConnection dbConnection, int policyId) where T : Customer
        {
            _logger.LogInformation($"Getting customers for policy '{policyId}'...");
            var dalObject = await _dbContext.GetPolicyCustomersAsync(dbConnection, policyId);
            return _objectDocumentSerializer.Deserialize<DalModels.Customer, T>(dalObject);
        }

        public async Task<int> AddCustomerPolicy(IDbTransaction dbTransaction, int customerId, int policyId)
        {
            _logger.LogInformation($"Adding customer '{customerId}' to policy '{policyId}'...");
            return await _dbContext.AddCustomerPolicy(dbTransaction, customerId, policyId);
        }

        public async Task<int> RemoveCustomerPolicy(IDbTransaction dbTransaction, int customerId, int policyId)
        {
            _logger.LogInformation($"Removing customer '{customerId}' from policy '{policyId}'...");
            return await _dbContext.RemoveCustomerPolicy(dbTransaction, customerId, policyId);

        }

        public async Task<int> AddCustomerPolicyBenefit(IDbTransaction dbTransaction, int customerId, int policyId, int benefitId)
        {
            _logger.LogInformation($"Adding customer '{customerId}' to policy '{policyId}' benefit '{benefitId}'...");
            return await _dbContext.AddCustomerPolicyBenefit(dbTransaction, customerId, policyId, benefitId);
        }

        public async Task<int> RemoveCustomerPolicyBenefit(IDbTransaction dbTransaction, int customerId, int policyId, int benefitId)
        {
            _logger.LogInformation($"Removing customer '{customerId}' from policy '{policyId}' benefit '{benefitId}'...");
            return await _dbContext.RemoveCustomerPolicyBenefit(dbTransaction, customerId, policyId, benefitId);

        }
    }
}
