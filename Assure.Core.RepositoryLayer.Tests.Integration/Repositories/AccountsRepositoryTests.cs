using System;
using System.Threading.Tasks;
using Assure.Core.RepositoryLayer.Interfaces.Repositories;
using Assure.Core.RepositoryLayer.Tests.Integration.TestModels;
using Assure.Core.Shared.Interfaces.Models;
using GenFu;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;


namespace Assure.Core.RepositoryLayer.Tests.Integration.Repositories
{
    public class AccountsCoreRepositoryTests : AbstractCoreRepositoryTests<IAccountsRepository, TestAccount>
    {
        public AccountsCoreRepositoryTests()
        {

        }

        [Fact]
        public async void DeleteAccountAsync()
        {
            _logger.LogInformation("DeleteAccountAsync");
            var newItem = A.New<TestAccount>();
            newItem.Name = $"{newItem.Name} ({Guid.NewGuid()})";
            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    var insertedItem = await _repository.InsertAsync(dbTransaction, newItem);
                    Assert.NotNull(insertedItem);
                    Assert.NotEqual(0, insertedItem.AccountId);

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
        public async void GetAccountAsync_UsingConnection()
        {
            _logger.LogInformation("GetAccountAsync_UsingConnection");
            var newItem = A.New<TestAccount>();
            newItem.Name = $"{newItem.Name} ({Guid.NewGuid()})";
            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    newItem = await _repository.InsertAsync(dbTransaction, newItem);
                    dbTransaction.Commit();

                    Assert.NotNull(newItem);
                    Assert.NotEqual(0, newItem.AccountId);
                }
                catch
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }

            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var result = await _repository.GetAsync<TestAccount>(dbConnection, newItem.AccountId);
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async void GetAccountAsync_UsingTransaction()
        {
            _logger.LogInformation("GetAccountAsync_UsingTransaction");
            var newItem = A.New<TestAccount>();
            newItem.Name = $"{newItem.Name} ({Guid.NewGuid()})";
            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    newItem = await _repository.InsertAsync(dbTransaction, newItem);
                    dbTransaction.Commit();

                    Assert.NotNull(newItem);
                    Assert.NotEqual(0, newItem.AccountId);
                }
                catch
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }

            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                var result = await _repository.GetAsync<TestAccount>(dbTransaction, newItem.AccountId);
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async void GetAccountsAsync()
        {
            _logger.LogInformation("GetAccountsAsync");
            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var result = await _repository.GetAsync<TestAccount>(dbConnection);
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async void GetAccountsAsync_PageRequest()
        {
            _logger.LogInformation("GetAccountsAsync_PageRequest");
            var pageRequest = new PageRequest
            {
                PageIndex = 50,
                PageSize = 10000,
                SortBy = "AccountId",
                SortOrder = SortOrders.DESC
            };
            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var result = await _repository.GetAsync<TestAccount>(dbConnection, pageRequest);
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async void InsertAccountAsync()
        {
            _logger.LogInformation("InsertAccountAsync");
            var newItem = A.New<TestAccount>();
            newItem.Name = $"{newItem.Name} ({Guid.NewGuid()})";
            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    var result = await _repository.InsertAsync(dbTransaction, newItem);
                    dbTransaction.Commit();

                    Assert.NotNull(result);
                    Assert.NotEqual(0, result.AccountId);
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
        public async Task InsertAccountAsync_Multiple(int count)
        {
            _logger.LogInformation($"InsertAccountAsync_Multiple '{count}'...");
            var newItems = A.ListOf<TestAccount>(count);
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
                        Assert.NotEqual(0, result.AccountId);
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
        public async void UpdateAccountAsync()
        {
            _logger.LogInformation("UpdateAccountAsync");
            var newItem = A.New<TestAccount>();
            newItem.Name = $"{newItem.Name} ({Guid.NewGuid()})";
            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    var insertedItem = await _repository.InsertAsync(dbTransaction, newItem);
                    Assert.NotNull(insertedItem);
                    Assert.NotEqual(0, insertedItem.AccountId);

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