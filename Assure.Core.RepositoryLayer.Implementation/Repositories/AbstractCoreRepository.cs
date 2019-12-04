using Assure.Core.DataAccessLayer.Interfaces.DbContexts;
using Assure.Core.DataAccessLayer.Interfaces.Models;
using Assure.Core.RepositoryLayer.Implementation.Serialization;
using Assure.Core.Shared.Interfaces.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Assure.Core.RepositoryLayer.Implementation.Repositories
{
    public abstract class AbstractCoreRepository<TDbContext, TRepositoryModel, TDataModel> where TDbContext : ICoreDbContext<TDataModel> where TDataModel : ObjectDocumentContainer where TRepositoryModel : class
    {
        protected readonly ILogger _logger;
        protected readonly TDbContext _dbContext;
        protected readonly IObjectDocumentSerializer _objectDocumentSerializer;

        protected AbstractCoreRepository(ILogger logger, TDbContext dbContext, IObjectDocumentSerializer objectDocumentSerializer)
        {
            _logger = logger;
            _dbContext = dbContext;
            _objectDocumentSerializer = objectDocumentSerializer;
        }

        public async Task<IEnumerable<T>> GetAsync<T>(IDbConnection dbConnection) where T : class, TRepositoryModel
        {
            _logger.LogInformation($"Getting all {typeof(T)}...");
            var dalObject = await _dbContext.GetAllAsync(dbConnection);
            return _objectDocumentSerializer.Deserialize<TDataModel, T>(dalObject);
        }

        public async Task<PageResponse<T>> GetAsync<T>(IDbConnection dbConnection, PageRequest pageRequest) where T : class, TRepositoryModel
        {
            _logger.LogInformation($"Getting {typeof(TRepositoryModel)} page '{pageRequest}'...");
            var dalObject = await _dbContext.GetPageAsync(dbConnection, pageRequest);
            return _objectDocumentSerializer.Deserialize<TDataModel, T>(dalObject);
        }

        public async Task<T> GetAsync<T>(IDbConnection dbConnection, int key) where T : class, TRepositoryModel
        {
            _logger.LogInformation($"Getting {typeof(TRepositoryModel)} with ID '{key}'...");
            var dalObject = await _dbContext.GetAsync(dbConnection, key);
            return (null == dalObject) ? null : _objectDocumentSerializer.Deserialize<TDataModel, T>(dalObject);
        }
        public async Task<T> GetAsync<T>(IDbTransaction dbTransaction, int key) where T : class, TRepositoryModel
        {
            _logger.LogInformation($"Getting {typeof(TRepositoryModel)} with ID '{key}'...");
            var dalObject = await _dbContext.GetAsync(dbTransaction.Connection, key, dbTransaction);
            return (null == dalObject) ? null : _objectDocumentSerializer.Deserialize<TDataModel, T>(dalObject);
        }

        public async Task<int> GetCountAsync(IDbConnection dbConnection)
        {
            _logger.LogInformation($"Getting {typeof(TRepositoryModel)} count...");
            return await _dbContext.GetCountAsync(dbConnection);
        }

        public async Task<T> InsertAsync<T>(IDbTransaction dbTransaction, T repositoryModel) where T : class, TRepositoryModel
        {
            _logger.LogInformation($"Inserting {typeof(T)} '{repositoryModel}'...");
            var dalObject = _objectDocumentSerializer.Serialize<T, TDataModel>(repositoryModel);
            var insertedDalObject = await _dbContext.InsertAsync(dbTransaction, dalObject);
            return _objectDocumentSerializer.Deserialize<TDataModel, T>(insertedDalObject);
        }

        public async Task<bool> UpdateAsync<T>(IDbTransaction dbTransaction, T repositoryModel) where T : class, TRepositoryModel
        {
            _logger.LogInformation($"Updating {typeof(T)} '{repositoryModel}'...");
            var dalObject = _objectDocumentSerializer.Serialize<T, TDataModel>(repositoryModel);
            return await _dbContext.UpdateAsync(dbTransaction, dalObject);
        }

        public async Task<bool> DeleteAsync<T>(IDbTransaction dbTransaction, T repositoryModel) where T : class, TRepositoryModel
        {
            _logger.LogInformation($"Deleting {typeof(T)} '{repositoryModel}'...");
            var dalObject = _objectDocumentSerializer.Serialize<T, TDataModel>(repositoryModel);
            return await _dbContext.DeleteAsync(dbTransaction, dalObject);
        }
    }
}