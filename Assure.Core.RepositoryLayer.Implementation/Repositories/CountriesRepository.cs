using Assure.Core.DataAccessLayer.Interfaces.DbConnections;
using Assure.Core.DataAccessLayer.Interfaces.DbContexts;
using Assure.Core.RepositoryLayer.Interfaces.Repositories;
using AutoMapper;
using Boot4ServiceCollection.Attributes;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Assure.Core.RepositoryLayer.Implementation.Repositories
{
    [AddSingleton(typeof(ICountriesRepository))]
    public sealed class CountriesRepository : AbstractReferenceRepository<RepositoryLayer.Interfaces.Models.Country, DataAccessLayer.Interfaces.Models.Country>, ICountriesRepository
    {
        public CountriesRepository(ILogger<CountriesRepository> logger, IDbConnectionFactory dbConnectionFactory, ICountriesDbContext dbContext, IMapper mapper, IMemoryCache memoryCache) :
            base(logger, dbConnectionFactory, dbContext, mapper, memoryCache)
        {

        }
    }
}