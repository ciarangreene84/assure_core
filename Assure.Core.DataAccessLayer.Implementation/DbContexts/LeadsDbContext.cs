using Assure.Core.DataAccessLayer.Interfaces;
using Assure.Core.DataAccessLayer.Interfaces.DbContexts;
using Assure.Core.DataAccessLayer.Interfaces.Models;
using AutoMapper;
using Boot4ServiceCollection.Attributes;
using Microsoft.Extensions.Logging;

namespace Assure.Core.DataAccessLayer.Implementation.DbContexts
{
    [AddScoped(typeof(ILeadsDbContext))]
    public sealed class LeadsDbContext : AbstractCoreDbContext<Lead, Models.Lead>, ILeadsDbContext
    {
        public LeadsDbContext(ILogger<LeadsDbContext> logger, IMapper mapper, IPageSortValidator pageSortValidator)
            : base(logger, mapper, pageSortValidator)
        {

        }
    }
}