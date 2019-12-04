using Assure.Core.DataAccessLayer.Interfaces.DbContexts;
using Assure.Core.RepositoryLayer.Implementation.Serialization;
using Assure.Core.RepositoryLayer.Interfaces.Repositories;
using Boot4ServiceCollection.Attributes;
using Microsoft.Extensions.Logging;

namespace Assure.Core.RepositoryLayer.Implementation.Repositories
{
    [AddScoped(typeof(IQuotesRepository))]
    public sealed class QuotesRepository : AbstractCoreRepository<IQuotesDbContext, RepositoryLayer.Interfaces.Models.Quote, DataAccessLayer.Interfaces.Models.Quote>, IQuotesRepository
    {
        public QuotesRepository(ILogger<QuotesRepository> logger, IObjectDocumentSerializer objectDocumentSerializer, IQuotesDbContext dbContext) :
            base(logger, dbContext, objectDocumentSerializer)
        {

        }
    }
}
