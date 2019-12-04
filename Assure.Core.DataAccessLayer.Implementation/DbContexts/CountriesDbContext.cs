using Assure.Core.DataAccessLayer.Interfaces.DbContexts;
using AutoMapper;
using Boot4ServiceCollection.Attributes;
using Microsoft.Extensions.Logging;

namespace Assure.Core.DataAccessLayer.Implementation.DbContexts
{
    [AddSingleton(typeof(ICountriesDbContext))]
    public sealed class CountriesDbContext : AbstractReferenceDbContext<Assure.Core.DataAccessLayer.Interfaces.Models.Country, Assure.Core.DataAccessLayer.Implementation.Models.Country>, ICountriesDbContext
    {
        public CountriesDbContext(ILogger<CountriesDbContext> logger, IMapper mapper) : base(logger, mapper)
        {

        }
    }
}