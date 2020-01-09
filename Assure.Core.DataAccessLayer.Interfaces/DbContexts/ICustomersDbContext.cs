using Assure.Core.DataAccessLayer.Interfaces.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Assure.Core.DataAccessLayer.Interfaces.DbContexts
{
    public interface ICustomersDbContext : ICoreDbContext<Customer>
    {
        Task<IEnumerable<Customer>> GetPolicyCustomersAsync(IDbConnection dbConnection, int policyId);
        Task<int> AddCustomerPolicy(IDbTransaction dbTransaction, int customerId, int policyId);
        Task<int> RemoveCustomerPolicy(IDbTransaction dbTransaction, int customerId, int policyId);
        Task<int> AddCustomerPolicyBenefit(IDbTransaction dbTransaction, int customerId, int policyId, int benefitId);
        Task<int> RemoveCustomerPolicyBenefit(IDbTransaction dbTransaction, int customerId, int policyId, int benefitId);
    }
}