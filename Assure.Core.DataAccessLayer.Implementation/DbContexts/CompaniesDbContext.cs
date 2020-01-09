using Assure.Core.DataAccessLayer.Interfaces;
using Assure.Core.DataAccessLayer.Interfaces.DbContexts;
using Assure.Core.DataAccessLayer.Interfaces.Models;
using AutoMapper;
using Boot4ServiceCollection.Attributes;
using Microsoft.Extensions.Logging;

namespace Assure.Core.DataAccessLayer.Implementation.DbContexts
{
    [AddScoped(typeof(ICompaniesDbContext))]
    public sealed class CompaniesDbContext : AbstractCoreDbContext<Company, Models.Company>, ICompaniesDbContext
    {
        public CompaniesDbContext(ILogger<CompaniesDbContext> logger, IMapper mapper, IPageSortValidator pageSortValidator)
            : base(logger, mapper, pageSortValidator)
        {

        }
    }
}