using Assure.Core.DataAccessLayer.Interfaces;
using Assure.Core.DataAccessLayer.Interfaces.DbContexts;
using Assure.Core.DataAccessLayer.Interfaces.Models;
using AutoMapper;
using Boot4ServiceCollection.Attributes;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Assure.Core.DataAccessLayer.Implementation.DbContexts
{
    [AddScoped(typeof(IPoliciesDbContext))]
    public sealed class PoliciesDbContext : AbstractCoreDbContext<Policy, Models.Policy>, IPoliciesDbContext
    {
        public PoliciesDbContext(ILogger<PoliciesDbContext> logger, IMapper mapper, IPageSortValidator pageSortValidator)
            : base(logger, mapper, pageSortValidator)
        {

        }

        public async Task<IEnumerable<Policy>> GetCustomerPoliciesAsync(IDbConnection dbConnection, int customerId)
        {
            Logger.LogInformation($"Getting policies for customer '{customerId}'...");
            var items = await dbConnection.QueryTableValueFunctionAsync<Implementation.Models.Policy>("[CoreFacade].[GetCustomerPolicies]", new { CustomerId = customerId });
            return Mapper.Map<IEnumerable<Policy>>(items);
        }
    }
}