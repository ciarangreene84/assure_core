using Assure.Core.DataAccessLayer.Interfaces;
using Assure.Core.DataAccessLayer.Interfaces.DbContexts;
using Assure.Core.DataAccessLayer.Interfaces.Models;
using AutoMapper;
using Boot4ServiceCollection.Attributes;
using Microsoft.Extensions.Logging;

namespace Assure.Core.DataAccessLayer.Implementation.DbContexts
{
    [AddScoped(typeof(IRequestsDbContext))]
    public sealed class RequestsDbContext : AbstractCoreDbContext<Request, Models.Request>, IRequestsDbContext
    {
        public RequestsDbContext(ILogger<RequestsDbContext> logger, IMapper mapper, IPageSortValidator pageSortValidator)
            : base(logger, mapper, pageSortValidator)
        {

        }
    }
}