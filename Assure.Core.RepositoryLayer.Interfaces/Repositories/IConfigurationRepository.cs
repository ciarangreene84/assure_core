using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Assure.Core.RepositoryLayer.Interfaces.Repositories
{
    public interface IConfigurationRepository<in TArchetype> where TArchetype : class
    {
        Task<IEnumerable<T>> GetAsync<T>() where T : class, TArchetype;
        Task<T> GetAsync<T>(int key) where T : class, TArchetype;
        Task<int> GetCountAsync();
        Task<T> InsertAsync<T>(IDbTransaction dbTransaction, T repositoryModel) where T : class, TArchetype;
        Task<bool> UpdateAsync<T>(IDbTransaction dbTransaction, T repositoryModel) where T : class, TArchetype;
        Task<bool> DeleteAsync<T>(IDbTransaction dbTransaction, T repositoryModel) where T : class, TArchetype;
    }
}
