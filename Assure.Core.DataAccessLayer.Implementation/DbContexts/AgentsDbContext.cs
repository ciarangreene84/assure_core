using Assure.Core.DataAccessLayer.Interfaces;
using Assure.Core.DataAccessLayer.Interfaces.DbContexts;
using AutoMapper;
using Boot4ServiceCollection.Attributes;
using Microsoft.Extensions.Logging;

namespace Assure.Core.DataAccessLayer.Implementation.DbContexts
{
    [AddScoped(typeof(IAgentsDbContext))]
    public sealed class AgentsDbContext : AbstractCoreDbContext<Interfaces.Models.Agent, Implementation.Models.Agent>, IAgentsDbContext
    {
        public AgentsDbContext(ILogger<AgentsDbContext> logger, IMapper mapper, IPageSortValidator pageSortValidator)
            : base(logger, mapper, pageSortValidator)
        {

        }
    }
}
