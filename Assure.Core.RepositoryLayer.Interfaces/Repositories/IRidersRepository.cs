using Assure.Core.RepositoryLayer.Interfaces.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Assure.Core.RepositoryLayer.Interfaces.Repositories
{
    public interface IBenefitsRepository : IConfigurationRepository<Benefit>
    {
        Task<IEnumerable<T>> GetProductBenefitsAsync<T>(IDbTransaction dbTransaction, int productId) where T : Benefit;

        Task<IEnumerable<T>> GetCustomerPolicyBenefitsAsync<T>(IDbTransaction dbTransaction, int customerId, int policyId) where T : Benefit;
    }
}