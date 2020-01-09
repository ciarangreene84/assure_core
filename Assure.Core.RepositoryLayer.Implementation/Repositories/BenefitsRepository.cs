using Assure.Core.DataAccessLayer.Interfaces.DbContexts;
using Assure.Core.RepositoryLayer.Implementation.Serialization;
using Assure.Core.RepositoryLayer.Interfaces.Models;
using Assure.Core.RepositoryLayer.Interfaces.Repositories;
using Assure.Core.Shared.Interfaces.Models;
using Boot4ServiceCollection.Attributes;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Assure.Core.DataAccessLayer.Interfaces.DbConnections;
using Microsoft.Extensions.Caching.Memory;
using DalModels = Assure.Core.DataAccessLayer.Interfaces.Models;

namespace Assure.Core.RepositoryLayer.Implementation.Repositories
{
    [AddScoped(typeof(IBenefitsRepository))]
    public sealed class BenefitsRepository : AbstractConfigurationRepository<IBenefitsDbContext, RepositoryLayer.Interfaces.Models.Benefit, DataAccessLayer.Interfaces.Models.Benefit>, IBenefitsRepository
    {
        public BenefitsRepository(ILogger<BenefitsRepository> logger, IDbConnectionFactory dbConnectionFactory, IBenefitsDbContext dbContext, IObjectDocumentSerializer objectDocumentSerializer, IMemoryCache memoryCache) :
            base(logger, dbConnectionFactory, dbContext, objectDocumentSerializer, memoryCache)
        {

        }
        
        public async Task<IEnumerable<T>> GetProductBenefitsAsync<T>(IDbTransaction dbTransaction, int productId) where T : Benefit
        {
            _logger.LogInformation($"Getting benefit for product Id '{productId}'...");
            var dalObject = await _dbContext.GetProductBenefitsAsync(dbTransaction.Connection, productId, dbTransaction);
            return (null == dalObject) ? null : _objectDocumentSerializer.Deserialize<DalModels.Benefit, T>(dalObject);
        }

        public async Task<IEnumerable<T>> GetCustomerPolicyBenefitsAsync<T>(IDbTransaction dbTransaction, int customerId, int policyId) where T : Benefit
        {
            _logger.LogInformation($"Getting benefits for customer Id '{customerId}' policy Id '{policyId}'...");
            var dalObject = await _dbContext.GetCustomerPolicyBenefitsAsync(dbTransaction.Connection, customerId, policyId);
            return (null == dalObject) ? null : _objectDocumentSerializer.Deserialize<DalModels.Benefit, T>(dalObject);
        }
    }
}
