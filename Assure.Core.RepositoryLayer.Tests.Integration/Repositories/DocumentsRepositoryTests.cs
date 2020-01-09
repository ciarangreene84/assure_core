using Assure.Core.RepositoryLayer.Interfaces.Models;
using Assure.Core.RepositoryLayer.Interfaces.Repositories;
using Assure.Core.Shared.Interfaces.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using Assure.Core.DataAccessLayer.Implementation;
using Assure.Core.DataAccessLayer.Implementation.DbConnections;
using Assure.Core.DataAccessLayer.Interfaces.DbConnections;
using Assure.Core.RepositoryLayer.Tests.Integration.Fixtures;
using Assure.Core.RepositoryLayer.Tests.Integration.TestModels;
using AutoMapper;
using GenFu;
using Microsoft.Extensions.Configuration;
using NLog.Extensions.Logging;
using Xunit;

namespace Assure.Core.RepositoryLayer.Tests.Integration.Repositories
{
    public class DocumentsCoreRepositoryTests : IClassFixture<PoliciesFixture>, IClassFixture<ClaimsFixture>
    {
        private readonly ILogger<DocumentsCoreRepositoryTests> _logger;
        private readonly Guid _userId;
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly IDocumentsRepository _repository;

        private readonly IClaimsRepository _claimsRepository;
        private readonly PoliciesFixture _policiesFixture;
        private readonly ClaimsFixture _claimsFixture;

        public DocumentsCoreRepositoryTests(PoliciesFixture policiesFixture, ClaimsFixture claimsFixture)
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
            _logger = loggerFactory.CreateLogger<DocumentsCoreRepositoryTests>();
            _dbConnectionFactory = serviceProvider.GetService<IDbConnectionFactory>();
            _repository = serviceProvider.GetService<IDocumentsRepository>();
            
            _claimsRepository = serviceProvider.GetService<IClaimsRepository>();
            _policiesFixture = policiesFixture;
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

            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var result = await _repository.GetDocumentsAsync(dbConnection, pageRequest);
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async void GetDocumentAsync()
        {
            _logger.LogInformation("GetDocumentAsync...");
            var pageRequest = new PageRequest()
            {
                SortBy = "LastWrite",
                SortOrder = SortOrders.DESC,
                PageIndex = 0,
                PageSize = 50
            };

            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var result = await _repository.GetDocumentsAsync(dbConnection, pageRequest);
                Assert.NotNull(result);

                var firstDocument = result.Items.First();
                Assert.Empty(firstDocument.Data);

                var document = await _repository.GetDocumentAsync(dbConnection, firstDocument.DocumentId);
                Assert.NotEmpty(document.Data);
            }
        }

        [Fact]
        public async void InsertDocumentAsync()
        {
            _logger.LogInformation("InsertDocumentAsync...");
            var newItem = new Document()
            {
                Name = $"REPOSITORY_TEST_{DateTime.Now:yyyyMMddHHmmssfff}.jpg",
                Type = "jpg",
                Data = await File.ReadAllBytesAsync("./TestFiles/Test.jpg")
            };

            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    var result = await _repository.InsertDocumentAsync(dbTransaction, newItem);
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
        public async void GetClaimDocumentsAsync()
        {
            _logger.LogInformation("GetClaimDocumentsAsync...");
            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var result = await _repository.GetClaimDocumentsAsync(dbConnection, 12016);
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async void AddPolicyDocument()
        {
            _logger.LogInformation("AddPolicyDocument...");
            var policyId = _policiesFixture.Items.First().PolicyId;

            Document insertedDocument;
            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                var newItem = new Document()
                {
                    Name = $"REPOSITORY_TEST_{DateTime.Now:yyyyMMddHHmmssfff}.jpg",
                    Type = "jpg",
                    Data = await File.ReadAllBytesAsync("./TestFiles/Test.jpg")
                };
                insertedDocument = await _repository.InsertDocumentAsync(dbTransaction, newItem);
                await _repository.AddPolicyDocument(dbTransaction, policyId, insertedDocument.DocumentId);
                dbTransaction.Commit();

            }
            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var result = await _repository.GetPolicyDocumentsAsync(dbConnection, policyId);
                Assert.NotNull(result);
                Assert.Contains(result, candidate => candidate.DocumentId == insertedDocument.DocumentId);
            }
        }

        [Fact]
        public async void RemovePolicyDocument()
        {
            _logger.LogInformation("AddPolicyDocument...");
            var policyId = _policiesFixture.Items.First().PolicyId;

            Document insertedDocument;
            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                var newItem = new Document()
                {
                    Name = $"REPOSITORY_TEST_{DateTime.Now:yyyyMMddHHmmssfff}.jpg",
                    Type = "jpg",
                    Data = await File.ReadAllBytesAsync("./TestFiles/Test.jpg")
                };
                insertedDocument = await _repository.InsertDocumentAsync(dbTransaction, newItem);
                await _repository.AddPolicyDocument(dbTransaction, policyId, insertedDocument.DocumentId);
                dbTransaction.Commit();

            }
            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var result = await _repository.GetPolicyDocumentsAsync(dbConnection, policyId);
                Assert.NotNull(result);
                Assert.Contains(result, candidate => candidate.DocumentId == insertedDocument.DocumentId);
            }

            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                await _repository.RemovePolicyDocument(dbTransaction, policyId, insertedDocument.DocumentId);
                dbTransaction.Commit();
            }

            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var result = await _repository.GetPolicyDocumentsAsync(dbConnection, policyId);
                Assert.NotNull(result);
                Assert.DoesNotContain(result, candidate => candidate.DocumentId == insertedDocument.DocumentId);
            }
        }

        [Fact]
        public async void GetPolicyDocumentsAsync()
        {
            _logger.LogInformation("GetPolicyDocumentsAsync...");
            var policyId = _policiesFixture.Items.First().PolicyId;
            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var result = await _repository.GetPolicyDocumentsAsync(dbConnection, policyId);
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async void AddClaimDocument()
        {
            _logger.LogInformation("AddClaimDocument...");
            {
                var newItem = A.New<TestClaim>();
                var policy = _policiesFixture.Items[A.Random.Next(0, _policiesFixture.Items.Count - 1)];
                newItem.PolicyId = policy.PolicyId;
                using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
                {
                    try
                    {
                        newItem = await _claimsRepository.InsertAsync(dbTransaction, newItem);
                        dbTransaction.Commit();

                        Assert.NotNull(newItem);
                        Assert.NotEqual(0, newItem.ClaimId);
                    }
                    catch
                    {
                        dbTransaction.Rollback();
                        throw;
                    }
                }
            }

            int claimId = -1;
            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                claimId = (await _claimsRepository.GetAsync<TestClaim>(dbConnection)).Last().ClaimId;
            }

            Document insertedDocument;
            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                var newItem = new Document()
                {
                    Name = $"REPOSITORY_TEST_{DateTime.Now:yyyyMMddHHmmssfff}.jpg",
                    Type = "jpg",
                    Data = await File.ReadAllBytesAsync("./TestFiles/Test.jpg")
                };
                insertedDocument = await _repository.InsertDocumentAsync(dbTransaction, newItem);
                await _repository.AddClaimDocument(dbTransaction, claimId, insertedDocument.DocumentId);
                dbTransaction.Commit();

            }
            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var result = await _repository.GetClaimDocumentsAsync(dbConnection, claimId);
                Assert.NotNull(result);
                Assert.Contains(result, candidate => candidate.DocumentId == insertedDocument.DocumentId);
            }
        }



        [Fact]
        public async void RemoveClaimDocument()
        {
            _logger.LogInformation("AddClaimDocument...");
            var claimId = _claimsFixture.Items.First().ClaimId;

            Document insertedDocument;
            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                var newItem = new Document()
                {
                    Name = $"REPOSITORY_TEST_{DateTime.Now:yyyyMMddHHmmssfff}.jpg",
                    Type = "jpg",
                    Data = await File.ReadAllBytesAsync("./TestFiles/Test.jpg")
                };
                insertedDocument = await _repository.InsertDocumentAsync(dbTransaction, newItem);
                await _repository.AddClaimDocument(dbTransaction, claimId, insertedDocument.DocumentId);
                dbTransaction.Commit();
            }
            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var result = await _repository.GetClaimDocumentsAsync(dbConnection, claimId);
                Assert.NotNull(result);
                Assert.Contains(result, candidate => candidate.DocumentId == insertedDocument.DocumentId);
            }

            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                await _repository.RemoveClaimDocument(dbTransaction, claimId, insertedDocument.DocumentId);
                dbTransaction.Commit();
            }

            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var result = await _repository.GetClaimDocumentsAsync(dbConnection, claimId);
                Assert.NotNull(result);
                Assert.DoesNotContain(result, candidate => candidate.DocumentId == insertedDocument.DocumentId);
            }
        }
    }
}