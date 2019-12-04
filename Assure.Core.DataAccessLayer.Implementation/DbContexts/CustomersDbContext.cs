using Assure.Core.DataAccessLayer.Interfaces;
using Assure.Core.DataAccessLayer.Interfaces.DbContexts;
using Assure.Core.DataAccessLayer.Interfaces.Models;
using AutoMapper;
using Boot4ServiceCollection.Attributes;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Assure.Core.DataAccessLayer.Implementation.DbContexts
{
    [AddScoped(typeof(ICustomersDbContext))]
    public sealed class CustomersDbContext : AbstractCoreDbContext<Customer, Models.Customer>, ICustomersDbContext
    {
        public CustomersDbContext(ILogger<CustomersDbContext> logger, IMapper mapper, IPageSortValidator pageSortValidator)
            : base(logger, mapper, pageSortValidator)
        {

        }

        public async Task<IEnumerable<Customer>> GetPolicyCustomersAsync(IDbConnection dbConnection, int policyId)
        {
            Logger.LogInformation($"Getting customers for policy '{policyId}'...");
            var items = await dbConnection.QueryTableValueFunctionAsync<Implementation.Models.Customer>("[CoreFacade].[GetPolicyCustomers]", new { PolicyId = policyId });
            return Mapper.Map<IEnumerable<Customer>>(items);
        }
        public Task<int> AddCustomerPolicy(IDbTransaction dbTransaction, int customerId, int policyId)
        {
            Logger.LogInformation($"Adding policy with Id '{policyId}' to customer with Id '{customerId}'...");
            return dbTransaction.Connection.ExecuteAsync("[CoreFacade].[AddCustomerPolicy] @CustomerId, @PolicyId", new { CustomerId = customerId, PolicyId = policyId }, dbTransaction);
        }

        public Task<int> RemoveCustomerPolicy(IDbTransaction dbTransaction, int customerId, int policyId)
        {
            Logger.LogInformation($"Removing policy with Id '{policyId}' from customer with Id '{customerId}'...");
            return dbTransaction.Connection.ExecuteAsync("[CoreFacade].[RemoveCustomerPolicy] @CustomerId, @PolicyId", new { CustomerId = customerId, PolicyId = policyId }, dbTransaction);
        }

        public Task<int> AddCustomerPolicyBenefit(IDbTransaction dbTransaction, int customerId, int policyId, int benefitId)
        {
            Logger.LogInformation($"Adding benefit with Id '{benefitId}' and policy with Id '{policyId}' to customer with Id '{customerId}'...");
            return dbTransaction.Connection.ExecuteAsync("[CoreFacade].[AddCustomerPolicyBenefit] @CustomerId, @PolicyId, @BenefitId", new { CustomerId = customerId, PolicyId = policyId, BenefitId = benefitId }, dbTransaction);
        }

        public Task<int> RemoveCustomerPolicyBenefit(IDbTransaction dbTransaction, int customerId, int policyId, int benefitId)
        {
            Logger.LogInformation($"Removing benefit with Id '{benefitId}' and policy with Id '{policyId}' to customer with Id '{customerId}'...");
            return dbTransaction.Connection.ExecuteAsync("[CoreFacade].[RemoveCustomerPolicyBenefit] @CustomerId, @PolicyId, @BenefitId", new { CustomerId = customerId, PolicyId = policyId, BenefitId = benefitId }, dbTransaction);
        }
    }
}