using Assure.Core.DataAccessLayer.Interfaces;
using Assure.Core.DataAccessLayer.Interfaces.DbContexts;
using Assure.Core.DataAccessLayer.Interfaces.Models;
using AutoMapper;
using Boot4ServiceCollection.Attributes;
using Microsoft.Extensions.Logging;

namespace Assure.Core.DataAccessLayer.Implementation.DbContexts
{
    [AddScoped(typeof(ICardsDbContext))]
    public sealed class CardsDbContext : AbstractCoreDbContext<Card, Models.Card>, ICardsDbContext
    {
        public CardsDbContext(ILogger<CardsDbContext> logger, IMapper mapper, IPageSortValidator pageSortValidator)
            : base(logger, mapper, pageSortValidator)
        {

        }
    }
}
