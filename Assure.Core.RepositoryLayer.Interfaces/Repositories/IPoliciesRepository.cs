using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Assure.Core.RepositoryLayer.Interfaces.Models;

namespace Assure.Core.RepositoryLayer.Interfaces.Repositories
{
    public interface IPoliciesRepository : ICoreRepository<Policy>
    {
        Task<IEnumerable<T>> GetCustomerPoliciesAsync<T>(IDbConnection dbConnection, int customerId) where T : Policy;

    }
}