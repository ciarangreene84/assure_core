using Assure.Core.DataAccessLayer.Interfaces.DbContexts;
using Assure.Core.RepositoryLayer.Implementation.Serialization;
using Assure.Core.RepositoryLayer.Interfaces.Repositories;
using Boot4ServiceCollection.Attributes;
using Microsoft.Extensions.Logging;

namespace Assure.Core.RepositoryLayer.Implementation.Repositories
{
    [AddScoped(typeof(ICompaniesRepository))]
    public sealed class CompaniesRepository : AbstractCoreRepository<ICompaniesDbContext, RepositoryLayer.Interfaces.Models.Company, DataAccessLayer.Interfaces.Models.Company>, ICompaniesRepository
    {
        public CompaniesRepository(ILogger<CompaniesRepository> logger, IObjectDocumentSerializer objectDocumentSerializer, ICompaniesDbContext dbContext) :
            base(logger, dbContext, objectDocumentSerializer)
        {

        }
    }
}