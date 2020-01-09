using Assure.Core.RepositoryLayer.Interfaces.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Assure.Core.RepositoryLayer.Interfaces.Repositories
{
    public interface ICustomersRepository : ICoreRepository<Customer>
    {
        Task<IEnumerable<T>> GetPolicyCustomersAsync<T>(IDbConnection dbConnection, int policyId) where T : Customer;
        Task<int> AddCustomerPolicy(IDbTransaction dbTransaction, int customerId, int policyId);
        Task<int> RemoveCustomerPolicy(IDbTransaction dbTransaction, int customerId, int policyId);

        Task<int> AddCustomerPolicyBenefit(IDbTransaction dbTransaction, int customerId, int policyId, int benefitId);
        Task<int> RemoveCustomerPolicyBenefit(IDbTransaction dbTransaction, int customerId, int policyId, int benefitId);
    }
}