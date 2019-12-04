using Assure.Core.DataAccessLayer.Interfaces.DbContexts;
using Assure.Core.RepositoryLayer.Implementation.Serialization;
using Assure.Core.RepositoryLayer.Interfaces.Repositories;
using Boot4ServiceCollection.Attributes;
using Microsoft.Extensions.Logging;

namespace Assure.Core.RepositoryLayer.Implementation.Repositories
{
    [AddScoped(typeof(IRequestsRepository))]
    public sealed class RequestsRepository : AbstractCoreRepository<IRequestsDbContext, RepositoryLayer.Interfaces.Models.Request, DataAccessLayer.Interfaces.Models.Request>, IRequestsRepository
    { 
        public RequestsRepository(ILogger<RequestsRepository> logger, IObjectDocumentSerializer objectDocumentSerializer, IRequestsDbContext dbContext) :
            base(logger, dbContext, objectDocumentSerializer)
        {

        }
    }
}
