using Assure.Core.DataAccessLayer.Interfaces.DbContexts;
using Assure.Core.RepositoryLayer.Implementation.Serialization;
using Assure.Core.RepositoryLayer.Interfaces.Repositories;
using Boot4ServiceCollection.Attributes;
using Microsoft.Extensions.Logging;

namespace Assure.Core.RepositoryLayer.Implementation.Repositories
{
    [AddScoped(typeof(ILeadsRepository))]
    public sealed class LeadsRepository : AbstractCoreRepository<ILeadsDbContext, RepositoryLayer.Interfaces.Models.Lead, DataAccessLayer.Interfaces.Models.Lead>, ILeadsRepository
    {
        public LeadsRepository(ILogger<LeadsRepository> logger, IObjectDocumentSerializer objectDocumentSerializer,
            ILeadsDbContext dbContext) :
            base(logger, dbContext, objectDocumentSerializer)
        {

        }
    }
}