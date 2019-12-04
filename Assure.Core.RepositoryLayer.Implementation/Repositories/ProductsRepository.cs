using Assure.Core.DataAccessLayer.Interfaces.DbConnections;
using Assure.Core.DataAccessLayer.Interfaces.DbContexts;
using Assure.Core.RepositoryLayer.Implementation.Serialization;
using Assure.Core.RepositoryLayer.Interfaces.Repositories;
using Boot4ServiceCollection.Attributes;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Assure.Core.RepositoryLayer.Implementation.Repositories
{
    [AddSingleton(typeof(IProductsRepository))]
    public sealed class ProductsRepository : AbstractConfigurationRepository<IProductsDbContext, RepositoryLayer.Interfaces.Models.Product, DataAccessLayer.Interfaces.Models.Product>, IProductsRepository
    {
        public ProductsRepository(ILogger<ProductsRepository> logger, IDbConnectionFactory dbConnectionFactory, IProductsDbContext dbContext, IObjectDocumentSerializer objectDocumentSerializer, IMemoryCache memoryCache) :
            base(logger, dbConnectionFactory, dbContext, objectDocumentSerializer, memoryCache)
        {

        }
    }
}
