using Assure.Core.Utilities.Implementation.Services;
using Assure.Core.Utilities.Interfaces.Services;
using System;
using Xunit;

namespace Assure.Core.Utilities.Tests.Unit.Services
{
    public class DayCalculatorTests
    {
        private readonly IDayCalculator _dayCalculator;

        public DayCalculatorTests()
        {
            _dayCalculator = new DayCalculator();
        }

        [Fact]
        public void CalculateDaysInclusive()
        {
            var result = _dayCalculator.CalculateDaysInclusive(DateTimeOffset.Now, DateTimeOffset.Now);
            Assert.Equal(1, result);
        }

        [Fact]
        public void CalculateDaysInclusive_Invalid()
        {
            Assert.Throws<ArgumentException>(() => _dayCalculator.CalculateDaysInclusive(DateTimeOffset.Now.AddSeconds(1), DateTimeOffset.Now));
        }
    }
}
