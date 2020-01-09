using Assure.Core.Utilities.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;
using Xunit;

namespace Assure.Core.Utilities.Tests.Integration.Services
{
    public class DayCalculatorTests
    {
        private readonly ILogger<DayCalculatorTests> _logger;
        private readonly IDayCalculator _dayCalculator;

        public DayCalculatorTests()
        {
            var services = new ServiceCollection();
            services.AddOptions();
            services.AddLogging(configure => configure.AddNLog());
            NLog.LogManager.LoadConfiguration("nlog.config");

            services.Boot();

            var serviceProvider = services.BuildServiceProvider();
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

            _logger = loggerFactory.CreateLogger<DayCalculatorTests>();
            _dayCalculator = serviceProvider.GetService<IDayCalculator>();
        }

        [Fact]
        public void CalculateDaysInclusive()
        {
            _logger.LogInformation("CalculateDaysInclusive...");
            var result = _dayCalculator.CalculateDaysInclusive(DateTimeOffset.Now, DateTimeOffset.Now);
            Assert.Equal(1, result);
        }

        [Fact]
        public void CalculateDaysInclusive_Invalid()
        {
            _logger.LogInformation("CalculateDaysInclusive_Invalid...");
            Assert.Throws<ArgumentException>(() => _dayCalculator.CalculateDaysInclusive(DateTimeOffset.Now.AddSeconds(1), DateTimeOffset.Now));
        }
    }
}
