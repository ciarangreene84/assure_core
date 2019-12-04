using Assure.Core.DataAccessLayer.Interfaces.DbContexts;
using Assure.Core.RepositoryLayer.Implementation.Serialization;
using Assure.Core.RepositoryLayer.Interfaces.Repositories;
using Boot4ServiceCollection.Attributes;
using Microsoft.Extensions.Logging;

namespace Assure.Core.RepositoryLayer.Implementation.Repositories
{
    [AddScoped(typeof(IInvoiceRepository))]
    public sealed class InvoiceRepository : AbstractCoreRepository<IInvoicesDbContext, RepositoryLayer.Interfaces.Models.Invoice, DataAccessLayer.Interfaces.Models.Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(ILogger<InvoiceRepository> logger, IObjectDocumentSerializer objectDocumentSerializer, IInvoicesDbContext dbContext) :
            base(logger, dbContext, objectDocumentSerializer)
        {

        }
    }
}
