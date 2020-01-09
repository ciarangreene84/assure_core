using Assure.Core.RepositoryLayer.Interfaces.Repositories;
using Assure.Core.RepositoryLayer.Tests.Integration.TestModels;
using Assure.Core.Shared.Interfaces.Models;
using GenFu;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Xunit;


namespace Assure.Core.RepositoryLayer.Tests.Integration.Repositories
{
    public class RequestsCoreRepositoryTests : AbstractCoreRepositoryTests<IRequestsRepository, TestRequest>
    {
        public RequestsCoreRepositoryTests()
        {

        }

        [Fact]
        public async void GetRequestsAsync()
        {
            _logger.LogInformation("GetRequestsAsync");
            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var result = await _repository.GetAsync<TestRequest>(dbConnection);
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async void GetRequestsAsync_PageRequest()
        {
            _logger.LogInformation("GetRequestsAsync_PageRequest");
            var pageRequest = new PageRequest
            {
                PageIndex = 50,
                PageSize = 10000,
                SortBy = "RequestId",
                SortOrder = SortOrders.DESC
            };
            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var result = await _repository.GetAsync<TestRequest>(dbConnection, pageRequest);
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async void InsertRequestAsync()
        {
            _logger.LogInformation("InsertRequestAsync");
            var newItem = A.New<TestRequest>();
            newItem.Type = $"{newItem.Type} ({Guid.NewGuid()}) ({DateTime.Now:yyyyMMddHHmmssfff})".Substring(0, 32);
            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    var result = await _repository.InsertAsync(dbTransaction, newItem);
                    dbTransaction.Commit();

                    Assert.NotNull(result);
                    Assert.NotEqual(0, result.RequestId);
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
        [InlineData(100)]
        //[InlineData(1000)]
        //[InlineData(100000)]
        public async void InsertRequestAsync_Multiple(int count)
        {
            _logger.LogInformation($"InsertRequestAsync_Multiple '{count}'...");
            var newItems = A.ListOf<TestRequest>(count);
            foreach (var newItem in newItems)
                using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
                {
                    try
                    {
                        newItem.Type = $"{newItem.Type} ({Guid.NewGuid()}) ({DateTime.Now:yyyyMMddHHmmssfff})".Substring(0, 32);
                        var result = await _repository.InsertAsync(dbTransaction, newItem);
                        dbTransaction.Commit();

                        Assert.NotNull(result);
                        Assert.NotEqual(0, result.RequestId);
                    }
                    catch
                    {
                        dbTransaction.Rollback();
                        throw;
                    }
                }
        }

        [Fact]
        public async void UpdateRequestAsync()
        {
            _logger.LogInformation("UpdateRequestAsync");
            var newItem = A.New<TestRequest>();
            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    var insertedItem = await _repository.InsertAsync(dbTransaction, newItem);
                    Assert.NotNull(insertedItem);
                    Assert.NotEqual(0, insertedItem.RequestId);

                    insertedItem.Type =
                        $"{newItem.Type} ({Guid.NewGuid()}) ({DateTime.Now:yyyyMMddHHmmssfff}) (Updated)".Substring(0, 32);
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