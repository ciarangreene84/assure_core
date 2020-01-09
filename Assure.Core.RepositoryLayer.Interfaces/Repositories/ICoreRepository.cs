using Assure.Core.Shared.Interfaces.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Assure.Core.RepositoryLayer.Interfaces.Repositories
{
    public interface ICoreRepository<in TArchetype> where TArchetype : class
    {
        Task<IEnumerable<T>> GetAsync<T>(IDbConnection dbConnection) where T : class, TArchetype;
        Task<PageResponse<T>> GetAsync<T>(IDbConnection dbConnection, PageRequest pageRequest) where T : class, TArchetype;
        Task<T> GetAsync<T>(IDbConnection dbConnection, int key) where T : class, TArchetype;
        Task<T> GetAsync<T>(IDbTransaction dbTransaction, int key) where T : class, TArchetype;
        Task<int> GetCountAsync(IDbConnection dbConnection);
        Task<T> InsertAsync<T>(IDbTransaction dbTransaction, T repositoryModel) where T : class, TArchetype;
        Task<bool> UpdateAsync<T>(IDbTransaction dbTransaction, T repositoryModel) where T : class, TArchetype;
        Task<bool> DeleteAsync<T>(IDbTransaction dbTransaction, T repositoryModel) where T : class, TArchetype;
    }
}