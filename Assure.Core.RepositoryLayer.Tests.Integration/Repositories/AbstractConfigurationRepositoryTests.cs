using Assure.Core.DataAccessLayer.Implementation;
using Assure.Core.DataAccessLayer.Interfaces.DbConnections;
using Assure.Core.RepositoryLayer.Interfaces.Repositories;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;
using System.Linq;
using Xunit;


namespace Assure.Core.RepositoryLayer.Tests.Integration.Repositories
{
    public abstract class AbstractConfigurationRepositoryTests<TInterface, TModel> where TInterface : IConfigurationRepository<TModel> where TModel : class
    {
        protected readonly ILogger<AbstractConfigurationRepositoryTests<TInterface, TModel>> _logger;

        protected readonly IDbConnectionFactory _dbConnectionFactory;
        protected readonly IConfigurationRepository<TModel> _repository;
        protected readonly Guid _userId;

        protected AbstractConfigurationRepositoryTests()
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

            var serviceProvider = services.BuildServiceProvider();
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            _logger = loggerFactory.CreateLogger<AbstractConfigurationRepositoryTests<TInterface, TModel>>();
            _dbConnectionFactory = serviceProvider.GetService<IDbConnectionFactory>();
            _repository = serviceProvider.GetService<TInterface>();
        }

        [Fact]
        public async void GetAsync()
        {
            var result = await _repository.GetAsync<TModel>();
            Assert.NotNull(result);
        }

        [Fact]
        public async void GetCountAsync()
        {
            _logger.LogInformation("GetCountAsync...");
            var allCount = (await _repository.GetAsync<TModel>()).Count();
            var result = await _repository.GetCountAsync();
            Assert.Equal(allCount, result);
        }
    }
}
