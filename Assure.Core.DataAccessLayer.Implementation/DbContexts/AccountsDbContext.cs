using Assure.Core.DataAccessLayer.Interfaces;
using Assure.Core.DataAccessLayer.Interfaces.DbContexts;
using Assure.Core.DataAccessLayer.Interfaces.Models;
using AutoMapper;
using Boot4ServiceCollection.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Assure.Core.DataAccessLayer.Implementation.DbContexts
{
    [AddScoped(typeof(IAccountsDbContext))]
    public sealed class AccountsDbContext : AbstractCoreDbContext<Account, Models.Account>, IAccountsDbContext
    {
        public AccountsDbContext(ILogger<AccountsDbContext> logger, IMapper mapper, IPageSortValidator pageSortValidator) :
            base(logger, mapper, pageSortValidator)
        {

        }
    }
}