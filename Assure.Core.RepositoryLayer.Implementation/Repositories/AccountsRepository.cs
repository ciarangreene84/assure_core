using Assure.Core.DataAccessLayer.Interfaces.DbContexts;
using Assure.Core.RepositoryLayer.Implementation.Serialization;
using Assure.Core.RepositoryLayer.Interfaces.Repositories;
using Boot4ServiceCollection.Attributes;
using Microsoft.Extensions.Logging;

namespace Assure.Core.RepositoryLayer.Implementation.Repositories
{
    [AddScoped(typeof(IAccountsRepository))]
    public class AccountsRepository : AbstractCoreRepository<IAccountsDbContext, RepositoryLayer.Interfaces.Models.Account, DataAccessLayer.Interfaces.Models.Account>, IAccountsRepository
    {
        public AccountsRepository(ILogger<AccountsRepository> logger, IAccountsDbContext dbContext, IObjectDocumentSerializer objectDocumentSerializer) :
            base(logger, dbContext, objectDocumentSerializer)
        {

        }
    }
}