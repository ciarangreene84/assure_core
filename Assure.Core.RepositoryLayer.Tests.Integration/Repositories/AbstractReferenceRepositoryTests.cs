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
    public abstract class AbstractReferenceRepositoryTests<TInterface, TModel> where TInterface : IReferenceRepository<TModel> where TModel : class
    {
        protected readonly ILogger<AbstractReferenceRepositoryTests<TInterface, TModel>> _logger;

        protected readonly IDbConnectionFactory _dbConnectionFactory;
        protected readonly TInterface _repository;
        protected readonly Guid _userId;

        protected AbstractReferenceRepositoryTests()
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
            _logger = loggerFactory.CreateLogger<AbstractReferenceRepositoryTests<TInterface, TModel>>();
            _dbConnectionFactory = serviceProvider.GetService<IDbConnectionFactory>();
            _repository = serviceProvider.GetService<TInterface>();
        }

        [Fact]
        public async void GetAsync()
        {
            _logger.LogInformation("GetAsync...");
            var result = await _repository.GetAsync();
            Assert.NotNull(result);
        }

        [Fact]
        public async void GetAsync_Thrice()
        {
            _logger.LogInformation("GetAsync_Thrice...");
            var result1 = await _repository.GetAsync();
            Assert.NotNull(result1);

            var result2 = await _repository.GetAsync();
            Assert.NotNull(result2);

            var result3 = await _repository.GetAsync();
            Assert.NotNull(result3);
        }
    }
}
