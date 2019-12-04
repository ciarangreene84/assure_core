using Assure.Core.RepositoryLayer.Interfaces.Repositories;
using Assure.Core.RepositoryLayer.Tests.Integration.TestModels;
using Assure.Core.Shared.Interfaces.Models;
using GenFu;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Assure.Core.RepositoryLayer.Tests.Integration.Repositories
{
    public class AgentsCoreRepositoryTests : AbstractCoreRepositoryTests<IAgentsRepository, TestAgent>
    {
        public AgentsCoreRepositoryTests()
        {

        }

        [Fact]
        public async void DeleteAgentAsync()
        {
            _logger.LogInformation("DeleteAgentAsync");
            var newItem = A.New<TestAgent>();
            newItem.Name = $"{newItem.Name} ({Guid.NewGuid()})";
            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    var insertedItem = await _repository.InsertAsync(dbTransaction, newItem);
                    Assert.NotNull(insertedItem);
                    Assert.NotEqual(0, insertedItem.AgentId);

                    var result = await _repository.DeleteAsync(dbTransaction, insertedItem);
                    Assert.True(result);
                    dbTransaction.Commit();
                }
                catch
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }
        }


        [Fact]
        public async void GetAgentAsync_UsingConnection()
        {
            _logger.LogInformation("GetAgentAsync_UsingConnection");
            var newItem = A.New<TestAgent>();
            newItem.Name = $"{newItem.Name} ({Guid.NewGuid()})";
            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    newItem = await _repository.InsertAsync(dbTransaction, newItem);
                    dbTransaction.Commit();

                    Assert.NotNull(newItem);
                    Assert.NotEqual(0, newItem.AgentId);
                }
                catch
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }

            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var result = await _repository.GetAsync<TestAgent>(dbConnection, newItem.AgentId);
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async void GetAgentAsync_UsingTransaction()
        {
            _logger.LogInformation("GetAgentAsync_UsingTransaction");
            var newItem = A.New<TestAgent>();
            newItem.Name = $"{newItem.Name} ({Guid.NewGuid()})";
            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    newItem = await _repository.InsertAsync(dbTransaction, newItem);
                    dbTransaction.Commit();

                    Assert.NotNull(newItem);
                    Assert.NotEqual(0, newItem.AgentId);
                }
                catch
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }

            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                var result = await _repository.GetAsync<TestAgent>(dbTransaction, newItem.AgentId);
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async void GetAgentsAsync()
        {
            _logger.LogInformation("GetAgentsAsync");
            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var result = await _repository.GetAsync<TestAgent>(dbConnection);
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async void GetAgentsAsync_PageRequest()
        {
            _logger.LogInformation("GetAgentsAsync_PageRequest");
            var pageRequest = new PageRequest
            {
                PageIndex = 50,
                PageSize = 10000,
                SortBy = "AgentId",
                SortOrder = SortOrders.DESC
            };
            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var result = await _repository.GetAsync<TestAgent>(dbConnection, pageRequest);
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async void InsertAgentAsync()
        {
            _logger.LogInformation("InsertAgentAsync");
            var newItem = A.New<TestAgent>();
            newItem.Name = $"{newItem.Name} ({Guid.NewGuid()})";
            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    var result = await _repository.InsertAsync(dbTransaction, newItem);
                    dbTransaction.Commit();

                    Assert.NotNull(result);
                    Assert.NotEqual(0, result.AgentId);
                }
                catch
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }
        }

        [Theory]
        [Trait("Category", "Bulk Load")]
        [InlineData(1000)]
        //[InlineData(100000)]
        public async Task InsertAgentAsync_Multiple(int count)
        {
            _logger.LogInformation($"InsertAgentAsync_Multiple '{count}'...");
            var newItems = A.ListOf<TestAgent>(count);
            foreach (var newItem in newItems)
            {
                using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
                {
                    try
                    {
                        count++;
                        newItem.Name = $"{newItem.Name} ({count}) ({Guid.NewGuid()})";
                        var result = await _repository.InsertAsync(dbTransaction, newItem);
                        dbTransaction.Commit();

                        Assert.NotNull(result);
                        Assert.NotEqual(0, result.AgentId);
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
        public async void UpdateAgentAsync()
        {
            _logger.LogInformation("UpdateAgentAsync");
            var newItem = A.New<TestAgent>();
            newItem.Name = $"{newItem.Name} ({Guid.NewGuid()})";
            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    var insertedItem = await _repository.InsertAsync(dbTransaction, newItem);
                    Assert.NotNull(insertedItem);
                    Assert.NotEqual(0, insertedItem.AgentId);

                    insertedItem.Name = $"{newItem.Name} (Updated: {DateTime.Now})";
                    var result = await _repository.UpdateAsync(dbTransaction, insertedItem);
                    Assert.True(result);
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