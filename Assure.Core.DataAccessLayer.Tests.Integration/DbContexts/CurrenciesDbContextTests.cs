using Assure.Core.DataAccessLayer.Interfaces.DbContexts;
using Assure.Core.DataAccessLayer.Interfaces.Models;

namespace Assure.Core.DataAccessLayer.Tests.Integration.DbContexts
{
    public class CurrenciesDbContextTests : AbstractReferenceDbContextTests<ICurrenciesDbContext, Currency>
    {
        public CurrenciesDbContextTests()
        {

        }
    }
}
