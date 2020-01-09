using Assure.Core.RepositoryLayer.Interfaces.Repositories;
using Assure.Core.RepositoryLayer.Tests.Integration.TestModels;
using Assure.Core.Shared.Interfaces.Models;
using GenFu;
using Microsoft.Extensions.Logging;
using System;
using Assure.Core.RepositoryLayer.Interfaces.Models;
using Xunit;


namespace Assure.Core.RepositoryLayer.Tests.Integration.Repositories
{
    public class CustomersCoreRepositoryTests : AbstractCoreRepositoryTests<ICustomersRepository, TestCustomer>
    {
        public CustomersCoreRepositoryTests()
        {

        }

        [Fact]
        public async void DeleteCustomerAsync()
        {
            _logger.LogInformation("DeleteCustomerAsync");
            var newItem = A.New<TestCustomer>();
            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    var insertedItem = await _repository.InsertAsync(dbTransaction, newItem);
                    Assert.NotNull(insertedItem);
                    Assert.NotEqual(0, insertedItem.CustomerId);

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
        public async void GetCustomersAsync()
        {
            _logger.LogInformation("GetCustomersAsync");
            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var result = await _repository.GetAsync<TestCustomer>(dbConnection);
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async void GetCustomersAsync_PageRequest()
        {
            _logger.LogInformation("GetCustomersAsync_PageRequest");
            var pageRequest = new PageRequest
            {
                PageIndex = 50,
                PageSize = 10000,
                SortBy = "CustomerId",
                SortOrder = SortOrders.DESC
            };
            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var result = await _repository.GetAsync<TestCustomer>(dbConnection, pageRequest);
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async void InsertCustomerAsync()
        {
            _logger.LogInformation("InsertCustomerAsync");
            var newItem = A.New<TestCustomer>();
            newItem.Name = $"{newItem.Name} ({Guid.NewGuid()}) ({DateTime.Now:yyyyMMddHHmmssfff})";
            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    var result = await _repository.InsertAsync(dbTransaction, newItem);
                    dbTransaction.Commit();

                    Assert.NotNull(result);
                    Assert.NotEqual(0, result.CustomerId);
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
        public async void InsertCustomerAsync_Multiple(int count)
        {
            _logger.LogInformation($"InsertCustomerAsync_Multiple '{count}'...");
            var newItems = A.ListOf<TestCustomer>(count);
            foreach (var newItem in newItems)
                using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
                {
                    try
                    {
                        newItem.Name = $"{newItem.Name} ({Guid.NewGuid()}) ({DateTime.Now:yyyyMMddHHmmssfff})";
                        var result = await _repository.InsertAsync(dbTransaction, newItem);
                        dbTransaction.Commit();

                        Assert.NotNull(result);
                        Assert.NotEqual(0, result.CustomerId);
                    }
                    catch
                    {
                        dbTransaction.Rollback();
                        throw;
                    }
                }
        }

        [Fact]
        public async void UpdateCustomerAsync()
        {
            _logger.LogInformation("UpdateCustomerAsync");
            var newItem = A.New<TestCustomer>();
            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    var insertedItem = await _repository.InsertAsync(dbTransaction, newItem);
                    Assert.NotNull(insertedItem);
                    Assert.NotEqual(0, insertedItem.CustomerId);

                    insertedItem.Name =
                        $"{newItem.Name} ({Guid.NewGuid()}) ({DateTime.Now:yyyyMMddHHmmssfff}) (Updated)";
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