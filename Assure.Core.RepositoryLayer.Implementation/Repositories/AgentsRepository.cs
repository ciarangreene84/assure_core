using Assure.Core.DataAccessLayer.Interfaces.DbContexts;
using Assure.Core.RepositoryLayer.Implementation.Serialization;
using Assure.Core.RepositoryLayer.Interfaces.Repositories;
using Boot4ServiceCollection.Attributes;
using Microsoft.Extensions.Logging;

namespace Assure.Core.RepositoryLayer.Implementation.Repositories
{
    [AddScoped(typeof(IAgentsRepository))]
    public class AgentsRepository : AbstractCoreRepository<IAgentsDbContext, RepositoryLayer.Interfaces.Models.Agent, DataAccessLayer.Interfaces.Models.Agent>, IAgentsRepository
    {
        public AgentsRepository(ILogger<AgentsRepository> logger, IAgentsDbContext dbContext, IObjectDocumentSerializer objectDocumentSerializer) :
            base(logger, dbContext, objectDocumentSerializer)
        {

        }
    }
}