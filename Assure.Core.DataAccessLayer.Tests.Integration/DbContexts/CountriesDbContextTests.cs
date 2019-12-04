using Assure.Core.DataAccessLayer.Interfaces.DbContexts;
using Assure.Core.DataAccessLayer.Interfaces.Models;

namespace Assure.Core.DataAccessLayer.Tests.Integration.DbContexts
{
    public class CountriesDbContextTests : AbstractReferenceDbContextTests<ICountriesDbContext, Country>
    {
        public CountriesDbContextTests()
        {

        }
    }
}
