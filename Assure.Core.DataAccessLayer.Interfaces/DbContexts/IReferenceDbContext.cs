using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Assure.Core.DataAccessLayer.Interfaces.DbContexts
{
    public interface IReferenceDbContext<TInterface>
    {
        Task<IEnumerable<TInterface>> GetAllAsync(IDbConnection dbConnection);
    }
}