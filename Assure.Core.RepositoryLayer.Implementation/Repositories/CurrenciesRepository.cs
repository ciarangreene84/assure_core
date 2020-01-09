using Assure.Core.DataAccessLayer.Interfaces.DbConnections;
using Assure.Core.DataAccessLayer.Interfaces.DbContexts;
using Assure.Core.RepositoryLayer.Interfaces.Repositories;
using AutoMapper;
using Boot4ServiceCollection.Attributes;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Assure.Core.RepositoryLayer.Implementation.Repositories
{
    [AddSingleton(typeof(ICurrenciesRepository))]
    public sealed class CurrenciesRepository : AbstractReferenceRepository<RepositoryLayer.Interfaces.Models.Currency, DataAccessLayer.Interfaces.Models.Currency>, ICurrenciesRepository
    {
        public CurrenciesRepository(ILogger<CurrenciesRepository> logger, IDbConnectionFactory dbConnectionFactory, ICurrenciesDbContext dbContext, IMapper mapper, IMemoryCache memoryCache) :
            base(logger, dbConnectionFactory, dbContext, mapper, memoryCache)
        {

        }
    }
}