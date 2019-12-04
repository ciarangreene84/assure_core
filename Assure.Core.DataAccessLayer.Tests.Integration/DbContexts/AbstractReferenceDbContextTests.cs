using Assure.Core.DataAccessLayer.Implementation;
using Assure.Core.DataAccessLayer.Interfaces.DbConnections;
using Assure.Core.DataAccessLayer.Interfaces.DbContexts;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;
using Xunit;

namespace Assure.Core.DataAccessLayer.Tests.Integration.DbContexts
{
    public abstract class AbstractReferenceDbContextTests<TInterface, TModel> where TInterface : IReferenceDbContext<TModel>
    {
        protected readonly ILogger<AbstractReferenceDbContextTests<TInterface, TModel>> _logger;

        protected readonly IConfiguration Configuration;
        protected readonly Guid UserId;
        protected readonly IDbConnectionFactory DbConnectionFactory;

        protected readonly TInterface _dbContext;

        protected AbstractReferenceDbContextTests()
        {
            GenFuConfigurator.Initialise();

            Configuration = new ConfigurationBuilder().AddJsonFile("AppSettings.json").Build();
            UserId = Guid.Parse("F1F86C06-6AA7-42E5-2EA9-08D61FB87E4D");

            var services = new ServiceCollection();
            services.AddOptions();
            services.AddLogging(configure => configure.AddNLog());
            NLog.LogManager.LoadConfiguration("nlog.config");

            services.Boot();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.Configure<AssureDataAccessLayerSettings>(Configuration.GetSection("AssureDataAccessLayerSettings"));

            var serviceProvider = services.BuildServiceProvider();
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            DbConnectionFactory = serviceProvider.GetService<IDbConnectionFactory>();

            _logger = loggerFactory.CreateLogger<AbstractReferenceDbContextTests<TInterface, TModel>>();
            _dbContext = serviceProvider.GetService<TInterface>();
        }

        [Fact]
        public async void GetAllAsync()
        {
            _logger.LogInformation("GetAllAsync...");
            using (var dbConnection = await DbConnectionFactory.OpenUserConnectionAsync(UserId))
            {
                var result = await _dbContext.GetAllAsync(dbConnection);
                Assert.NotNull(result);
            }
        }
    }
}
