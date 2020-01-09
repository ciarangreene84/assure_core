using Assure.Core.DataAccessLayer.Implementation;
using Assure.Core.DataAccessLayer.Interfaces.DbConnections;
using Assure.Core.DataAccessLayer.Interfaces.DbContexts;
using Assure.Core.DataAccessLayer.Interfaces.Models;
using Assure.Core.DataAccessLayer.Tests.Integration.Fixtures;
using Assure.Core.Shared.Interfaces.Models;
using AutoMapper;
using GenFu;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using Xunit;

namespace Assure.Core.DataAccessLayer.Tests.Integration.DbContexts
{
    public class DocumentsDbContextTests : IClassFixture<PoliciesFixture>, IClassFixture<ClaimsFixture>
    {
        private readonly ILogger<DocumentsDbContextTests> _logger;

        private readonly IConfiguration Configuration;
        private readonly Guid UserId;

        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly IDocumentsDbContext _dbContext;

        private readonly IPoliciesDbContext _policiesDbContext;
        private readonly PoliciesFixture _policiesFixture;

        private readonly IClaimsDbContext _claimsDbContext;
        private readonly ClaimsFixture _claimsFixture;

        public DocumentsDbContextTests(PoliciesFixture policiesFixture, ClaimsFixture claimsFixture)
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
            
            _logger = loggerFactory.CreateLogger<DocumentsDbContextTests>();
            _dbConnectionFactory = serviceProvider.GetService<IDbConnectionFactory>();
            _dbContext = serviceProvider.GetService<IDocumentsDbContext>();

            _policiesDbContext = serviceProvider.GetService<IPoliciesDbContext>();
            _policiesFixture = policiesFixture;
            
            _claimsDbContext = serviceProvider.GetService<IClaimsDbContext>();
            _claimsFixture = claimsFixture;
        }

        [Fact]
        public async void GetDocumentsAsync()
        {
            _logger.LogInformation("GetDocumentsAsync...");
            var pageRequest = new PageRequest()
            {
                SortBy = "LastWrite",
                SortOrder = SortOrders.DESC,
                PageIndex = 0,
                PageSize = 50
            };

            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(UserId))
            {
                var result = await _dbContext.GetDocumentsAsync(dbConnection, pageRequest);
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async void InsertDocumentAsync()
        {
            _logger.LogInformation("InsertDocumentAsync...");
            var newItem = new Document()
            {
                Name = $"DATA_ACCESS_LAYER_TEST_{DateTime.Now:yyyyMMddHHmmssfff}.jpg",
                Type = "jpg",
                Data = await File.ReadAllBytesAsync("./TestFiles/Test.jpg")
            };

            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(UserId))
            {
                try
                {
                    var result = await _dbContext.InsertDocumentAsync(dbTransaction, newItem);
                    dbTransaction.Commit();

                    Assert.NotNull(result);
                    Assert.NotEqual(newItem.DocumentId, result.DocumentId);
                }
                catch
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }
        }

        [Fact]
        public async void GetDocumentAsync()
        {
            _logger.LogInformation("GetDocumentAsync...");
            var newItem = new Document()
            {
                Name = $"DATA_ACCESS_LAYER_TEST_{DateTime.Now:yyyyMMddHHmmssfff}.jpg",
                Type = "jpg",
                Data = await File.ReadAllBytesAsync("./TestFiles/Test.jpg")
            };

            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(UserId))
            {
                try
                {
                    var result = await _dbContext.InsertDocumentAsync(dbTransaction, newItem);
                    dbTransaction.Commit();

                    Assert.NotNull(result);
                    Assert.NotEqual(newItem.DocumentId, result.DocumentId);
                }
                catch
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }

            var pageRequest = new PageRequest()
            {
                SortBy = "LastWrite",
                SortOrder = SortOrders.DESC,
                PageIndex = 0,
                PageSize = 50
            };

            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(UserId))
            {
                var result = await _dbContext.GetDocumentsAsync(dbConnection, pageRequest);
                Assert.NotNull(result);

                var firstDocument = result.Items.First();
                Assert.Empty(firstDocument.Data);

                var document = await _dbContext.GetDocumentAsync(dbConnection, firstDocument.DocumentId);
                Assert.NotEmpty(document.Data);
            }
        }

        [Fact]
        public async void AddClaimDocumentAsync()
        {
            _logger.LogInformation("AddClaimDocumentAsync...");
            {
                var newItem = A.New<Policy>();
                using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(UserId))
                {
                    try
                    {
                        var result = await _policiesDbContext.InsertAsync(dbTransaction, newItem);
                        dbTransaction.Commit();

                        Assert.NotNull(result);
                        Assert.NotEqual(0, result.PolicyId);

                    }
                    catch
                    {
                        dbTransaction.Rollback();
                        throw;
                    }
                }
            }

            Policy existingPolicy;
            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(UserId))
            {
                var pageRequest = new PageRequest()
                {
                    PageIndex = 0,
                    PageSize = 1,
                    SortBy = "PolicyId",
                    SortOrder = SortOrders.DESC
                };
                existingPolicy = (await _policiesDbContext.GetPageAsync(dbConnection, pageRequest)).Items.First();
            }

            {
                var newItem = A.New<Claim>();
                newItem.PolicyId = existingPolicy.PolicyId;
                using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(UserId))
                {
                    try
                    {
                        var result = await _claimsDbContext.InsertAsync(dbTransaction, newItem);
                        dbTransaction.Commit();

                        Assert.NotNull(result);
                        Assert.NotEqual(0, result.ClaimId);

                    }
                    catch
                    {
                        dbTransaction.Rollback();
                        throw;
                    }
                }
            }

            Claim existingClaim;
            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(UserId))
            {
                var pageRequest = new PageRequest()
                {
                    PageIndex = 0,
                    PageSize = 1,
                    SortBy = "ClaimId",
                    SortOrder = SortOrders.DESC
                };
                existingClaim = (await _claimsDbContext.GetPageAsync(dbConnection, pageRequest)).Items.First();
            }

            {
                var newItem = new Document()
                {
                    Name = $"DATA_ACCESS_LAYER_TEST_{DateTime.Now:yyyyMMddHHmmssfff}.jpg",
                    Type = "jpg",
                    Data = await File.ReadAllBytesAsync("./TestFiles/Test.jpg")
                };

                using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(UserId))
                {
                    try
                    {
                        var result = await _dbContext.InsertDocumentAsync(dbTransaction, newItem);
                        Assert.NotNull(result);
                        Assert.NotEqual(newItem.DocumentId, result.DocumentId);


                        await _dbContext.AddClaimDocumentAsync(dbTransaction, existingClaim.ClaimId,
                            result.DocumentId);

                        dbTransaction.Commit();
                    }
                    catch
                    {
                        dbTransaction.Rollback();
                        throw;
                    }
                }
            }
        }

        [Fact]
        public async void InsertPolicyDocumentAsync()
        {
            _logger.LogInformation("AddPolicyDocumentAsync...");
            {
                var newItem = A.New<Policy>();
                using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(UserId))
                {
                    try
                    {
                        var result = await _policiesDbContext.InsertAsync(dbTransaction, newItem);
                        dbTransaction.Commit();

                        Assert.NotNull(result);
                        Assert.NotEqual(0, result.PolicyId);

                    }
                    catch
                    {
                        dbTransaction.Rollback();
                        throw;
                    }
                }
            }

            Policy existingPolicy;
            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(UserId))
            {
                var pageRequest = new PageRequest()
                {
                    PageIndex = 0,
                    PageSize = 1,
                    SortBy = "PolicyId",
                    SortOrder = SortOrders.DESC
                };
                existingPolicy = (await _policiesDbContext.GetPageAsync(dbConnection, pageRequest)).Items.First();
            }

            {

                var newItem = new Document()
                {
                    Name = $"DATA_ACCESS_LAYER_TEST_{DateTime.Now:yyyyMMddHHmmssfff}.jpg",
                    Type = "jpg",
                    Data = await File.ReadAllBytesAsync("./TestFiles/Test.jpg")
                };


                using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(UserId))
                {
                    try
                    {
                        var result = await _dbContext.InsertDocumentAsync(dbTransaction, newItem);
                        Assert.NotNull(result);
                        Assert.NotEqual(newItem.DocumentId, result.DocumentId);


                        await _dbContext.AddPolicyDocumentAsync(dbTransaction, existingPolicy.PolicyId,
                            result.DocumentId);

                        dbTransaction.Commit();
                    }
                    catch
                    {
                        dbTransaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
