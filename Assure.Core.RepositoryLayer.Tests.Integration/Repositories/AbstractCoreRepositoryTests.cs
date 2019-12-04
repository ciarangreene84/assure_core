using Assure.Core.DataAccessLayer.Implementation;
using Assure.Core.DataAccessLayer.Interfaces.DbConnections;
using Assure.Core.RepositoryLayer.Interfaces.Repositories;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;
using Xunit;

namespace Assure.Core.RepositoryLayer.Tests.Integration.Repositories
{
    public abstract class AbstractCoreRepositoryTests<TInterface, TModel> where TInterface : ICoreRepository<TModel> where TModel : class
    {
        protected readonly ILogger<AbstractCoreRepositoryTests<TInterface, TModel>> _logger;

        protected readonly ServiceProvider _serviceProvider;
        protected readonly IDbConnectionFactory _dbConnectionFactory;
        protected readonly TInterface _repository;
        protected readonly Guid _userId;

        protected AbstractCoreRepositoryTests()
        {
            GenFuConfigurator.Initialise();

            _userId = Guid.Parse("F1F86C06-6AA7-42E5-2EA9-08D61FB87E4D");

            var configuration = new ConfigurationBuilder().AddJsonFile("AppSettings.json").Build();
            var services = new ServiceCollection();
            services.AddOptions();
            services.AddMemoryCache();
            services.AddLogging(configure => configure.AddNLog());
            NLog.LogManager.LoadConfiguration("nlog.config");

            services.Boot();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.Configure<AssureDataAccessLayerSettings>(configuration.GetSection("AssureDataAccessLayerSettings"));

            _serviceProvider = services.BuildServiceProvider();
            var loggerFactory = _serviceProvider.GetRequiredService<ILoggerFactory>();
            _logger = loggerFactory.CreateLogger<AbstractCoreRepositoryTests<TInterface, TModel>>();
            _dbConnectionFactory = _serviceProvider.GetService<IDbConnectionFactory>();
            _repository = _serviceProvider.GetService<TInterface>();
        }

        [Fact]
        public async void GetAllAsync()
        {
            _logger.LogInformation("GetAllAsync...");
            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var result = await _repository.GetAsync<TModel>(dbConnection);
                Assert.NotNull(result);
            }
        }
    }
}
