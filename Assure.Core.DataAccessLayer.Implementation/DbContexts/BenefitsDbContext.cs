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
    [AddScoped(typeof(IBenefitsDbContext))]
    public sealed class BenefitsDbContext : AbstractCoreDbContext<Interfaces.Models.Benefit, Implementation.Models.Benefit>, IBenefitsDbContext
    {
        public BenefitsDbContext(ILogger<BenefitsDbContext> logger, IMapper mapper, IPageSortValidator pageSortValidator) : 
            base(logger, mapper, pageSortValidator)
        {

        }

        public async Task<IEnumerable<Benefit>> GetProductBenefitsAsync(IDbConnection dbConnection, int productId, IDbTransaction dbTransaction = null)
        {
            Logger.LogInformation($"Getting benefits for product Id '{productId}'...");

            var items = await dbConnection.QueryTableValueFunctionAsync<Implementation.Models.Benefit>("[StaticFacade].[GetProductBenefits]", new { ProductId = productId }, dbTransaction: dbTransaction);
            return Mapper.Map<IEnumerable<Benefit>>(items);
        }

        public Task<int> AddProductBenefit(IDbTransaction dbTransaction, int productId, int benefitId)
        {
            Logger.LogInformation($"Adding benefit with Id '{benefitId}' to product with Id '{productId}'...");
            return dbTransaction.Connection.ExecuteAsync("[StaticFacade].[AddProductBenefit]", new { ProductId = productId, BenefitId = benefitId }, dbTransaction);
        }

        public Task<int> RemoveProductBenefit(IDbTransaction dbTransaction, int productId, int benefitId)
        {
            Logger.LogInformation($"Removing benefit with Id '{benefitId}' from product with Id '{productId}'...");
            return dbTransaction.Connection.ExecuteAsync("[StaticFacade].[RemoveProductBenefit]", new { ProductId = productId, BenefitId = benefitId }, dbTransaction);
        }


        public async Task<IEnumerable<Benefit>> GetCustomerPolicyBenefitsAsync(IDbConnection dbConnection, int customerId, int policyId)
        {
            Logger.LogInformation($"Getting benefits for customer Id '{customerId}' policy Id '{policyId}'...");

            var items = await dbConnection.QueryTableValueFunctionAsync<Implementation.Models.Benefit>("[CoreFacade].[GetCustomerPolicyBenefits]", new { CustomerId = customerId, PolicyId = policyId });
            return Mapper.Map<IEnumerable<Benefit>>(items);
        }
    }
}