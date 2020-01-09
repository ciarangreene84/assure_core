using Assure.Core.DataAccessLayer.Implementation;
using Assure.Core.DataAccessLayer.Interfaces.DbConnections;
using Assure.Core.DataAccessLayer.Interfaces.DbContexts;
using Assure.Core.DataAccessLayer.Interfaces.Models;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;
using System.Linq;
using Xunit;

namespace Assure.Core.DataAccessLayer.Tests.Integration.DbContexts
{
    public abstract class AbstractDbContextTests<TInterface, TModel> where TInterface : ICoreDbContext<TModel> where TModel : ObjectDocumentContainer
    {
        protected readonly ILogger<AbstractDbContextTests<TInterface, TModel>> _logger;

        protected readonly IConfiguration Configuration = new ConfigurationBuilder().AddJsonFile("AppSettings.json").Build();
        protected readonly Guid UserId = Guid.Parse("F1F86C06-6AA7-42E5-2EA9-08D61FB87E4D");
        protected readonly IServiceProvider ServiceProvider;
        protected readonly ILoggerFactory LoggerFactory;
        protected readonly IDbConnectionFactory DbConnectionFactory;
        
        protected readonly TInterface _dbContext;

        protected AbstractDbContextTests()
        {
            GenFuConfigurator.Initialise();

            var services = new ServiceCollection();
            services.AddOptions();
            services.AddLogging(configure => configure.AddNLog());
            NLog.LogManager.LoadConfiguration("nlog.config");

            services.Boot();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.Configure<AssureDataAccessLayerSettings>(Configuration.GetSection("AssureDataAccessLayerSettings"));

            ServiceProvider = services.BuildServiceProvider();
            LoggerFactory = ServiceProvider.GetRequiredService<ILoggerFactory>();
            DbConnectionFactory = ServiceProvider.GetService<IDbConnectionFactory>();

            _logger = LoggerFactory.CreateLogger<AbstractDbContextTests<TInterface, TModel>>();
            _dbContext = ServiceProvider.GetService<TInterface>();
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

        [Fact]
        public async void GetCountAsync()
        {
            _logger.LogInformation("GetCountAsync...");
            using (var dbConnection = await DbConnectionFactory.OpenUserConnectionAsync(UserId))
            {
                var allCount = (await _dbContext.GetAllAsync(dbConnection)).Count();
                var result = await _dbContext.GetCountAsync(dbConnection);
                Assert.Equal(allCount, result);
            }
        }
    }
}
