using Assure.Core.DataAccessLayer.Interfaces.DbContexts;
using Assure.Core.RepositoryLayer.Implementation.Serialization;
using Assure.Core.RepositoryLayer.Interfaces.Repositories;
using Boot4ServiceCollection.Attributes;
using Microsoft.Extensions.Logging;

namespace Assure.Core.RepositoryLayer.Implementation.Repositories
{
    [AddScoped(typeof(IClaimsRepository))]
    public sealed class ClaimsRepository : AbstractCoreRepository<IClaimsDbContext, RepositoryLayer.Interfaces.Models.Claim, DataAccessLayer.Interfaces.Models.Claim>, IClaimsRepository
    {
        public ClaimsRepository(ILogger<ClaimsRepository> logger, IObjectDocumentSerializer objectDocumentSerializer, IClaimsDbContext dbContext) :
            base(logger, dbContext, objectDocumentSerializer)
        {

        }
    }
}