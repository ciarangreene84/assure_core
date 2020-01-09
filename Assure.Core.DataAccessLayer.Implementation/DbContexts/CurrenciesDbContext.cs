using Assure.Core.DataAccessLayer.Interfaces.DbContexts;
using AutoMapper;
using Boot4ServiceCollection.Attributes;
using Microsoft.Extensions.Logging;

namespace Assure.Core.DataAccessLayer.Implementation.DbContexts
{
    [AddSingleton(typeof(ICurrenciesDbContext))]
    public sealed class CurrenciesDbContext : AbstractReferenceDbContext<Assure.Core.DataAccessLayer.Interfaces.Models.Currency, Assure.Core.DataAccessLayer.Implementation.Models.Currency>, ICurrenciesDbContext
    {
        public CurrenciesDbContext(ILogger<CurrenciesDbContext> logger, IMapper mapper) : base(logger, mapper)
        {

        }
    }
}