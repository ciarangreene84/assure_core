using Assure.Core.DataAccessLayer.Interfaces.DbContexts;
using Assure.Core.DataAccessLayer.Interfaces.Models;
using Assure.Core.Shared.Interfaces.Models;
using GenFu;
using Microsoft.Extensions.Logging;
using System;
using Xunit;


namespace Assure.Core.DataAccessLayer.Tests.Integration.DbContexts
{
    public class LeadsDbContextTests : AbstractDbContextTests<ILeadsDbContext, Lead>
    {
        public LeadsDbContextTests()
        {

        }

        [Theory]
        [InlineData(1000)]
        public async void InsertLeadAsync_Multiple(int count)
        {
            _logger.LogInformation($"InsertLeadAsync_Multiple '{count}'...");

            var objectHash = count;
            using (var dbConnection = await DbConnectionFactory.OpenUserConnectionAsync(UserId))
            {
                objectHash += await _dbContext.GetCountAsync(dbConnection);
            }

            var newItems = A.ListOf<Lead>(count);
            foreach (var newItem in newItems)
            {
                newItem.Name = $"{newItem.Name} ({DateTime.Now:yyyyMMddHHmmssfff} - {Guid.NewGuid()})";
                newItem.ObjectDocument = $"{{\"Test\": \"{newItem.Name}\"}}";
                newItem.ObjectHash = objectHash++;
                using (var dbTransaction = await DbConnectionFactory.BeginUserTransactionAsync(UserId))
                {
                    try
                    {
                        var result = await _dbContext.InsertAsync(dbTransaction, newItem);
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
        }

        [Fact]
        public async void DeleteLeadAsync()
        {
            _logger.LogInformation("DeleteLeadAsync...");
            var newItem = A.New<Lead>();
            newItem.Name = $"{newItem.Name} ({DateTime.Now:yyyyMMddHHmmssfff} - {Guid.NewGuid()})";
            newItem.ObjectDocument = $"{{\"Test\": \"{newItem.Name}\"}}";
            using (var dbTransaction = await DbConnectionFactory.BeginUserTransactionAsync(UserId))
            {
                try
                {
                    newItem = await _dbContext.InsertAsync(dbTransaction, newItem);
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

            using (var dbTransaction = await DbConnectionFactory.BeginUserTransactionAsync(UserId))
            {
                try
                {
                    var result = await _dbContext.DeleteAsync(dbTransaction, newItem);
                    dbTransaction.Commit();
                    Assert.True(result);
                }
                catch
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }

            using (var dbConnection = await DbConnectionFactory.OpenUserConnectionAsync(UserId))
            {
                var result = await _dbContext.GetAsync(dbConnection, newItem.LeadId);
                Assert.Null(result);
            }
        }

        [Fact]
        public async void GetLeadsAsync()
        {
            _logger.LogInformation("GetLeadsAsync...");
            using (var dbConnection = await DbConnectionFactory.OpenUserConnectionAsync(UserId))
            {
                var result = await _dbContext.GetAllAsync(dbConnection);
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async void GetLeadsAsync_PageRequest()
        {
            _logger.LogInformation("GetLeadsAsync_PageRequest...");
            var pageRequest = new PageRequest
            {
                SortBy = "LeadId",
                SortOrder = SortOrders.DESC,
                PageIndex = 4,
                PageSize = 50
            };

            using (var dbConnection = await DbConnectionFactory.OpenUserConnectionAsync(UserId))
            {
                var result = await _dbContext.GetPageAsync(dbConnection, pageRequest);
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async void GetLeadAsync_DbConnection()
        {
            _logger.LogInformation("GetLeadAsync_DbConnection...");
            var newItem = A.New<Lead>();
            newItem.Name = $"{newItem.Name} ({DateTime.Now:yyyyMMddHHmmssfff} - {Guid.NewGuid()})";
            newItem.ObjectDocument = $"{{\"Test\": \"{newItem.Name}\"}}";
            using (var dbTransaction = await DbConnectionFactory.BeginUserTransactionAsync(UserId))
            {
                try
                {
                    newItem = await _dbContext.InsertAsync(dbTransaction, newItem);
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

            using (var dbConnection = await DbConnectionFactory.OpenUserConnectionAsync(UserId))
            {
                var result = await _dbContext.GetAsync(dbConnection, newItem.LeadId);

                Assert.NotNull(result);
                Assert.Equal(newItem.LeadId, result.LeadId);
            }
        }

        [Fact]
        public async void GetLeadAsync_DbTransaction()
        {
            _logger.LogInformation("GetLeadAsync_DbTransaction...");
            var newItem = A.New<Lead>();
            newItem.Name = $"{newItem.Name} ({DateTime.Now:yyyyMMddHHmmssfff} - {Guid.NewGuid()})";
            newItem.ObjectDocument = $"{{\"Test\": \"{newItem.Name}\"}}";
            using (var dbTransaction = await DbConnectionFactory.BeginUserTransactionAsync(UserId))
            {
                try
                {
                    newItem = await _dbContext.InsertAsync(dbTransaction, newItem);
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

            using (var dbTransaction = await DbConnectionFactory.BeginUserTransactionAsync(UserId))
            {
                var result =
                    await _dbContext.GetAsync(dbTransaction.Connection, newItem.LeadId, dbTransaction);

                Assert.NotNull(result);
                Assert.Equal(newItem.LeadId, result.LeadId);
            }
        }

        [Fact]
        public async void GetLeadCountAsync()
        {
            _logger.LogInformation("GetLeadCountAsync...");
            using (var dbConnection = await DbConnectionFactory.OpenUserConnectionAsync(UserId))
            {
                var result = await _dbContext.GetCountAsync(dbConnection);
                Assert.NotEqual(0, result);
            }
        }

        [Fact]
        public async void InsertLeadAsync()
        {
            _logger.LogInformation("InsertLeadAsync...");
            var newItem = A.New<Lead>();
            newItem.Name = $"{newItem.Name} ({DateTime.Now:yyyyMMddHHmmssfff} - {Guid.NewGuid()})";
            newItem.ObjectDocument = $"{{\"Test\": \"{newItem.Name}\"}}";
            using (var dbTransaction = await DbConnectionFactory.BeginUserTransactionAsync(UserId))
            {
                try
                {
                    var result = await _dbContext.InsertAsync(dbTransaction, newItem);
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
        public async void UpdateLeadAsync()
        {
            _logger.LogInformation("UpdateLeadAsync...");
            var newItem = A.New<Lead>();
            newItem.Name = $"{newItem.Name} ({DateTime.Now:yyyyMMddHHmmssfff} - {Guid.NewGuid()})";
            newItem.ObjectDocument = $"{{\"Test\": \"{newItem.Name}\"}}";
            using (var dbTransaction = await DbConnectionFactory.BeginUserTransactionAsync(UserId))
            {
                try
                {
                    newItem = await _dbContext.InsertAsync(dbTransaction, newItem);
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

            using (var dbTransaction = await DbConnectionFactory.BeginUserTransactionAsync(UserId))
            {
                try
                {
                    var itemToUpdate = A.New<Lead>();
                    itemToUpdate.LeadId = newItem.LeadId;
                    itemToUpdate.Name = $"{itemToUpdate.Name} ({DateTime.Now:yyyyMMddHHmmssfff} - {Guid.NewGuid()})";
                    itemToUpdate.ObjectDocument = $"{{\"Test\": \"{itemToUpdate.Name}\"}}";
                    var result = await _dbContext.UpdateAsync(dbTransaction, itemToUpdate);
                    dbTransaction.Commit();
                    Assert.True(result);
                }
                catch
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }

            using (var dbConnection = await DbConnectionFactory.OpenUserConnectionAsync(UserId))
            {
                var updatedItem = await _dbContext.GetAsync(dbConnection, newItem.LeadId);
                Assert.NotNull(updatedItem);
                Assert.NotEqual(updatedItem.Name, newItem.Name);
            }
        }
    }
}