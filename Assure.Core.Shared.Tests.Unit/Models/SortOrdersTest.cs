using Assure.Core.Shared.Interfaces.Models;
using Xunit;

namespace Assure.Core.Shared.Tests.Unit.Models
{
    public class SortOrdersTest
    {
        [Fact]
        public void ASC()
        {
            var result = SortOrders.ASC.ToString();
            Assert.Equal("ASC", result);
        }

        [Fact]
        public void DESC()
        {
            var result = SortOrders.DESC.ToString();
            Assert.Equal("DESC", result);
        }
    }
}
