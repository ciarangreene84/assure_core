using Assure.Core.DataAccessLayer.Interfaces.DbContexts;
using Assure.Core.RepositoryLayer.Implementation.Serialization;
using Assure.Core.RepositoryLayer.Interfaces.Repositories;
using Boot4ServiceCollection.Attributes;
using Microsoft.Extensions.Logging;

namespace Assure.Core.RepositoryLayer.Implementation.Repositories
{
    [AddScoped(typeof(IPaymentsRepository))]
    public sealed class PaymentsRepository : AbstractCoreRepository<IPaymentsDbContext, RepositoryLayer.Interfaces.Models.Payment, DataAccessLayer.Interfaces.Models.Payment>, IPaymentsRepository
    {
        public PaymentsRepository(ILogger<PaymentsRepository> logger, IPaymentsDbContext dbContext, IObjectDocumentSerializer objectDocumentSerializer) :
            base(logger, dbContext, objectDocumentSerializer)
        {

        }
    }
}
