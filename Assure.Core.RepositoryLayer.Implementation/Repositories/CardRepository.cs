using Assure.Core.DataAccessLayer.Interfaces.DbContexts;
using Assure.Core.RepositoryLayer.Implementation.Serialization;
using Assure.Core.RepositoryLayer.Interfaces.Repositories;
using Boot4ServiceCollection.Attributes;
using Microsoft.Extensions.Logging;

namespace Assure.Core.RepositoryLayer.Implementation.Repositories
{
    [AddScoped(typeof(ICardsRepository))]
    public sealed class CardRepository : AbstractCoreRepository<ICardsDbContext, RepositoryLayer.Interfaces.Models.Card, DataAccessLayer.Interfaces.Models.Card>, ICardsRepository
    {
        public CardRepository(ILogger<CardRepository> logger, IObjectDocumentSerializer objectDocumentSerializer, ICardsDbContext dbContext) :
            base(logger, dbContext, objectDocumentSerializer)
        {

        }
    }
}
