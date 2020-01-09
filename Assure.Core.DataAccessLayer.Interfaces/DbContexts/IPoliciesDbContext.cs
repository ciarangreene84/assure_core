using Assure.Core.DataAccessLayer.Interfaces.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Assure.Core.DataAccessLayer.Interfaces.DbContexts
{
    public interface IPoliciesDbContext : ICoreDbContext<Policy>
    {
        Task<IEnumerable<Policy>> GetCustomerPoliciesAsync(IDbConnection dbConnection, int customerId);
    }
}