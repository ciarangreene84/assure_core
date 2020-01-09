using System;
using Assure.Core.RepositoryLayer.Interfaces.Repositories;
using Assure.Core.RepositoryLayer.Tests.Integration.TestModels;
using Assure.Core.Shared.Interfaces.Models;
using GenFu;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;


namespace Assure.Core.RepositoryLayer.Tests.Integration.Repositories
{
    public class LeadsCoreRepositoryTests : AbstractCoreRepositoryTests<ILeadsRepository, TestLead>
    {
        public LeadsCoreRepositoryTests()
        {

        }

        [Theory]
        [Trait("Category", "Bulk Load")]
        [InlineData(1000)]
        //[InlineData(100000)]
        public async void InsertLeadAsync_Multiple(int count)
        {
            _logger.LogInformation($"InsertLeadAsync_Multiple '{count}'...");
            var newItems = A.ListOf<TestLead>(count);
            foreach (var newItem in newItems)
                using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
                {
                    try
                    {
                        newItem.Name = $"{newItem.Name} ({DateTime.Now:yyyyMMddHHmmssfff} - {Guid.NewGuid()})";
                        var result = await _repository.InsertAsync(dbTransaction, newItem);
                        dbTransaction.Commit();

                        Assert.NotNull(result);
                        Assert.NotEqual(0, result.LeadId);
                    }
                    catch
                    {
                        dbTransaction.Rollback();
                        throw;
                    }
                }
        }

        [Fact]
        public async void DeleteLeadAsync()
        {
            _logger.LogInformation("DeleteLeadAsync");
            var newItem = A.New<TestLead>();
            newItem.Name = $"{newItem.Name} ({DateTime.Now:yyyyMMddHHmmssfff} - {Guid.NewGuid()})";
            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    newItem = await _repository.InsertAsync(dbTransaction, newItem);
                    dbTransaction.Commit();

                    Assert.NotNull(newItem);
                    Assert.NotEqual(0, newItem.LeadId);
                }
                catch
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }

            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    var result = await _repository.DeleteAsync(dbTransaction, newItem);
                    Assert.True(result);
                    dbTransaction.Commit();
                }
                catch
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }

            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var deleteItem = await _repository.GetAsync<TestLead>(dbConnection, newItem.LeadId);
                Assert.Null(deleteItem);
            }
        }

        [Fact]
        public async void GetLeadsAsync()
        {
            _logger.LogInformation("GetLeadsAsync");
            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var result = await _repository.GetAsync<TestLead>(dbConnection);
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async void GetLeadsAsync_PageRequest()
        {
            _logger.LogInformation("GetLeadsAsync_PageRequest");
            var pageRequest = new PageRequest
            {
                PageIndex = 50,
                PageSize = 10000,
                SortBy = "LeadId",
                SortOrder = SortOrders.DESC
            };
            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var result = await _repository.GetAsync<TestLead>(dbConnection, pageRequest);
                Assert.NotNull(result);
            }
        }


        [Fact]
        public async void GetLeadAsync()
        {
            _logger.LogInformation("GetLeadAsync");
            var newItem = A.New<TestLead>();
            newItem.Name = $"{newItem.Name} ({DateTime.Now:yyyyMMddHHmmssfff} - {Guid.NewGuid()})";
            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    newItem = await _repository.InsertAsync(dbTransaction, newItem);
                    dbTransaction.Commit();

                    Assert.NotNull(newItem);
                    Assert.NotEqual(0, newItem.LeadId);
                }
                catch
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }

            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var result = _repository.GetAsync<TestLead>(dbConnection, newItem.LeadId);
                result.Wait();
                Assert.NotNull(result.Result);
            }
        }

        [Fact]
        public async void GetLeadCountAsync()
        {
            _logger.LogInformation("GetLeadCountAsync");
            var newItem = A.New<TestLead>();
            newItem.Name = $"{newItem.Name} ({DateTime.Now:yyyyMMddHHmmssfff} - {Guid.NewGuid()})";
            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    newItem = await _repository.InsertAsync(dbTransaction, newItem);
                    dbTransaction.Commit();

                    Assert.NotNull(newItem);
                    Assert.NotEqual(0, newItem.LeadId);
                }
                catch
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }

            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var result = await _repository.GetCountAsync(dbConnection);
                Assert.NotEqual(0, result);
            }
        }

        [Fact]
        public async void InsertLeadAsync()
        {
            _logger.LogInformation("InsertLeadAsync");
            var newItem = A.New<TestLead>();
            newItem.Name = $"{newItem.Name} ({DateTime.Now:yyyyMMddHHmmssfff} - {Guid.NewGuid()})";
            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    newItem = await _repository.InsertAsync(dbTransaction, newItem);
                    dbTransaction.Commit();

                    Assert.NotNull(newItem);
                    Assert.NotEqual(0, newItem.LeadId);
                }
                catch
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }
        }

        [Fact]
        public async void UpdateLeadAsync()
        {
            _logger.LogInformation("UpdateLeadAsync");
            var newItem = A.New<TestLead>();
            newItem.Name = $"{newItem.Name} ({DateTime.Now:yyyyMMddHHmmssfff} - {Guid.NewGuid()})";
            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    newItem = await _repository.InsertAsync(dbTransaction, newItem);
                    dbTransaction.Commit();

                    Assert.NotNull(newItem);
                    Assert.NotEqual(0, newItem.LeadId);
                }
                catch
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }

            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    var itemToUpdate = A.New<TestLead>();
                    itemToUpdate.LeadId = newItem.LeadId;
                    itemToUpdate.Name = $"{newItem.Name} (Update)";
                    var result = await _repository.UpdateAsync(dbTransaction, itemToUpdate);
                    Assert.True(result);
                    dbTransaction.Commit();
                }
                catch
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }

            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var updatedItem = await _repository.GetAsync<TestLead>(dbConnection, newItem.LeadId);
                Assert.NotNull(updatedItem);
                Assert.Equal(updatedItem.LeadId, newItem.LeadId);
                Assert.NotEqual(updatedItem.Name, newItem.Name);
            }
        }
    }
}