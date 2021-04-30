using System.ComponentModel.DataAnnotations;
using Assure.Core.DataAccessLayer.Implementation;
using Assure.Core.DataAccessLayer.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Xunit;

namespace Assure.Core.DataAccessLayer.Tests.Unit
{
    public class PageSortValidatorTests
    {
        private readonly IPageSortValidator _pageSortValidator;

        public PageSortValidatorTests()
        {
            var services = new ServiceCollection();
            services.AddOptions();
            //configure NLog            
            NLog.LogManager.LoadConfiguration("nlog.config");
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddNLog();
            });

            services.Boot();

            var serviceProvider = services.BuildServiceProvider();
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

            _pageSortValidator = serviceProvider.GetService<IPageSortValidator>();
        }

        [Fact]
        public void ValidateSortByProperty()
        {
            _pageSortValidator.ValidateSortByProperty<TestClass>("SortBy");
        }

        [Fact]
        public void ValidateSortByProperty_Invalid()
        {
            var result = Assert.Throws<ValidationException>(() => _pageSortValidator.ValidateSortByProperty<TestClass>("Invalid"));
            Assert.Equal("Unknown property 'Invalid'.", result.Message);
        }
    }

    public class TestClass
    {
        public string SortBy { get; set; }
    }
}
