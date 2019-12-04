using Assure.Core.DataAccessLayer.Interfaces.Models;
using Assure.Core.Shared.Interfaces.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Assure.Core.DataAccessLayer.Interfaces.DbContexts
{
    public interface ICoreDbContext<TInterface> where TInterface : ObjectDocumentContainer
    {
        Task<IEnumerable<TInterface>> GetAllAsync(IDbConnection dbConnection, IDbTransaction dbTransaction = null);
        Task<PageResponse<TInterface>> GetPageAsync(IDbConnection dbConnection, PageRequest pageRequest);
        Task<TInterface> GetAsync(IDbConnection dbConnection, int key, IDbTransaction dbTransaction = null);
        Task<int> GetCountAsync(IDbConnection dbConnection);
        Task<TInterface> InsertAsync(IDbTransaction transaction, TInterface objectToInsert);
        Task<bool> UpdateAsync(IDbTransaction transaction, TInterface objectToUpdate);
        Task<bool> DeleteAsync(IDbTransaction transaction, TInterface objectToDelete);
    }
}