using Assure.Core.DataAccessLayer.Interfaces.DbConnections;
using Assure.Core.DataAccessLayer.Interfaces.DbContexts;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assure.Core.RepositoryLayer.Implementation.Repositories
{
    public class AbstractReferenceRepository<TRepositoryModel, TDataModel> where TDataModel : class where TRepositoryModel : class
    {
        private readonly ILogger _logger;
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly IReferenceDbContext<TDataModel> _dbContext;
        
        private readonly IMemoryCache _memoryCache;
        private readonly IMapper _mapper;

        private const string All = @"ALL";

        protected AbstractReferenceRepository(ILogger logger, IDbConnectionFactory dbConnectionFactory, IReferenceDbContext<TDataModel> dbContext, IMapper mapper, IMemoryCache memoryCache)
        {
            _logger = logger;

            _dbConnectionFactory = dbConnectionFactory;
            _dbContext = dbContext;
            _mapper = mapper;

            _memoryCache = memoryCache;
        }

        public async Task<IEnumerable<TRepositoryModel>> GetAsync()
        {
            _logger.LogInformation($"Getting all {typeof(TRepositoryModel)}...");
            return await _memoryCache.GetOrCreateAsync(CreateCacheKey<TRepositoryModel>(All), async entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromHours(8);
                using (var dbConnection = await _dbConnectionFactory.OpenConnectionAsync())
                {
                    var dalObject = await _dbContext.GetAllAsync(dbConnection);
                    return _mapper.Map<IEnumerable<TRepositoryModel>>(dalObject);
                }
            });
        }
        
        private string CreateCacheKey<T>(object key)
        {
            _logger.LogDebug($"Creating cache key for {typeof(T)} with {key}...");
            return $"{key}_{typeof(T)}";
        }
    }
}