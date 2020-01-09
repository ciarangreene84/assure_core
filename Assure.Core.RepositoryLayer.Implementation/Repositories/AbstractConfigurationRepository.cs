using Assure.Core.DataAccessLayer.Interfaces.DbConnections;
using Assure.Core.DataAccessLayer.Interfaces.DbContexts;
using Assure.Core.DataAccessLayer.Interfaces.Models;
using Assure.Core.RepositoryLayer.Implementation.Serialization;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Assure.Core.RepositoryLayer.Implementation.Repositories
{
    public abstract class AbstractConfigurationRepository<TDbContext, TRepositoryModel, TDataModel> where TDbContext : ICoreDbContext<TDataModel> where TDataModel : ObjectDocumentContainer where TRepositoryModel : class
    {
        protected readonly ILogger _logger;
        private readonly IDbConnectionFactory _dbConnectionFactory;
        protected readonly TDbContext _dbContext;
        protected readonly IObjectDocumentSerializer _objectDocumentSerializer;

        private readonly IMemoryCache _memoryCache;

        private const string All = @"ALL";
        private const string Count = @"COUNT";

        protected AbstractConfigurationRepository(ILogger logger, IDbConnectionFactory dbConnectionFactory, TDbContext dbContext, IObjectDocumentSerializer objectDocumentSerializer, IMemoryCache memoryCache)
        {
            _logger = logger;
            _dbConnectionFactory = dbConnectionFactory;
            _dbContext = dbContext;
            _objectDocumentSerializer = objectDocumentSerializer;
            _memoryCache = memoryCache;
        }

        public async Task<IEnumerable<T>> GetAsync<T>() where T : class, TRepositoryModel
        {
            _logger.LogInformation($"Getting all {typeof(T)}...");
            return await _memoryCache.GetOrCreateAsync(CreateCacheKey<TRepositoryModel>(All), async entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromHours(8);
                using (var dbConnection = await _dbConnectionFactory.OpenConnectionAsync())
                {
                    var dalObject = await _dbContext.GetAllAsync(dbConnection);
                    return _objectDocumentSerializer.Deserialize<TDataModel, T>(dalObject);
                }
            });
        }

        public async Task<T> GetAsync<T>(int key) where T : class, TRepositoryModel
        {
            _logger.LogInformation($"Getting {typeof(TRepositoryModel)} with ID '{key}'...");
            return await _memoryCache.GetOrCreateAsync(CreateCacheKey<TRepositoryModel>(key), async entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromHours(8);
                using (var dbConnection = await _dbConnectionFactory.OpenConnectionAsync())
                {
                    var dalObject = await _dbContext.GetAsync(dbConnection, key);
                    return (null == dalObject) ? null : _objectDocumentSerializer.Deserialize<TDataModel, T>(dalObject);
                }
            });
        }

        public async Task<int> GetCountAsync()
        {
            _logger.LogInformation($"Getting {typeof(TRepositoryModel)} count...");
            return await _memoryCache.GetOrCreateAsync(CreateCacheKey<TRepositoryModel>(Count), async entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromHours(8);
                using (var dbConnection = await _dbConnectionFactory.OpenConnectionAsync())
                {
                    return await _dbContext.GetCountAsync(dbConnection);
                }
            });
        }

        public async Task<T> InsertAsync<T>(IDbTransaction dbTransaction, T repositoryModel) where T : class, TRepositoryModel
        {
            _logger.LogInformation($"Inserting {typeof(T)} '{repositoryModel}'...");

            _memoryCache.Remove(CreateCacheKey<TRepositoryModel>(All));
            _memoryCache.Remove(CreateCacheKey<TRepositoryModel>(Count));

            var dalObject = _objectDocumentSerializer.Serialize<T, TDataModel>(repositoryModel);
            var insertedDalObject = await _dbContext.InsertAsync(dbTransaction, dalObject);
            return _objectDocumentSerializer.Deserialize<TDataModel, T>(insertedDalObject);
        }

        public async Task<bool> UpdateAsync<T>(IDbTransaction dbTransaction, T repositoryModel) where T : class, TRepositoryModel
        {
            _logger.LogInformation($"Updating {typeof(T)} '{repositoryModel}'...");

            _memoryCache.Remove(CreateCacheKey<TRepositoryModel>(All));
            _memoryCache.Remove(CreateCacheKey<TRepositoryModel>(Count));

            var dalObject = _objectDocumentSerializer.Serialize<T, TDataModel>(repositoryModel);
            return await _dbContext.UpdateAsync(dbTransaction, dalObject);
        }

        public async Task<bool> DeleteAsync<T>(IDbTransaction dbTransaction, T repositoryModel) where T : class, TRepositoryModel
        {
            _logger.LogInformation($"Deleting {typeof(T)} '{repositoryModel}'...");

            _memoryCache.Remove(CreateCacheKey<TRepositoryModel>(All));
            _memoryCache.Remove(CreateCacheKey<TRepositoryModel>(Count));

            var dalObject = _objectDocumentSerializer.Serialize<T, TDataModel>(repositoryModel);
            return await _dbContext.DeleteAsync(dbTransaction, dalObject);
        }
        
        private string CreateCacheKey<T>(object key)
        {
            _logger.LogDebug($"Creating cache key for {typeof(T)} with {key}...");
            return $"{typeof(T)}_{key}";
        }
    }
}
