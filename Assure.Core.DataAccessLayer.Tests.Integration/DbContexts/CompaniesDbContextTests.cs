using Assure.Core.DataAccessLayer.Interfaces.DbContexts;
using Assure.Core.DataAccessLayer.Interfaces.Models;
using Assure.Core.Shared.Interfaces.Models;
using GenFu;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Xunit;


namespace Assure.Core.DataAccessLayer.Tests.Integration.DbContexts
{
    public class CompaniesDbContextTests : AbstractDbContextTests<ICompaniesDbContext, Company>
    {
        public CompaniesDbContextTests()
        {

        }


        [Theory]
        [InlineData(1000)]
        public async void InsertCompanyAsync_Multiple(int count)
        {
            _logger.LogInformation($"InsertCompanyAsync_Multiple '{count}'...");

            var objectHash = count;
            using (var dbConnection = await DbConnectionFactory.OpenUserConnectionAsync(UserId))
            {
                objectHash += await _dbContext.GetCountAsync(dbConnection) + 1;
            }

            var newItems = A.ListOf<Company>(count);
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
                        Assert.NotEqual(0, result.CompanyId);
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
        public async void DeleteCompanyAsync()
        {
            _logger.LogInformation("DeleteCompanyAsync...");
            var newItem = A.New<Company>();
            newItem.Name = $"{newItem.Name} ({DateTime.Now:yyyyMMddHHmmssfff} - {Guid.NewGuid()})";
            newItem.ObjectDocument = $"{{\"Test\": \"{newItem.Name}\"}}";
            using (var dbTransaction = await DbConnectionFactory.BeginUserTransactionAsync(UserId))
            {
                try
                {
                    newItem = await _dbContext.InsertAsync(dbTransaction, newItem);
                    dbTransaction.Commit();

                    Assert.NotNull(newItem);
                    Assert.NotEqual(0, newItem.CompanyId);
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
                var result = await _dbContext.GetAsync(dbConnection, newItem.CompanyId);
                Assert.Null(result);
            }
        }

        [Fact]
        public async void GetCompaniesAsync()
        {
            _logger.LogInformation("GetCompaniesAsync...");
            using (var dbConnection = await DbConnectionFactory.OpenUserConnectionAsync(UserId))
            {
                var result = await _dbContext.GetAllAsync(dbConnection);
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async void GetCompaniesAsync_PageRequest()
        {
            _logger.LogInformation("GetCompaniesAsync_PageRequest...");
            var pageRequest = new PageRequest
            {
                SortBy = "CompanyId",
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
        public async void GetCompanyAsync_DbConnection()
        {
            _logger.LogInformation("GetCompanyAsync_DbConnection...");
            var newItem = A.New<Company>();
            newItem.Name = $"{newItem.Name} ({DateTime.Now:yyyyMMddHHmmssfff} - {Guid.NewGuid()})";
            newItem.ObjectDocument = $"{{\"Test\": \"{newItem.Name}\"}}";
            using (var dbTransaction = await DbConnectionFactory.BeginUserTransactionAsync(UserId))
            {
                try
                {
                    newItem = await _dbContext.InsertAsync(dbTransaction, newItem);
                    dbTransaction.Commit();

                    Assert.NotNull(newItem);
                    Assert.NotEqual(0, newItem.CompanyId);
                }
                catch
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }

            using (var dbConnection = await DbConnectionFactory.OpenUserConnectionAsync(UserId))
            {
                var result = await _dbContext.GetAsync(dbConnection, newItem.CompanyId);

                Assert.NotNull(result);
                Assert.Equal(newItem.CompanyId, result.CompanyId);
            }
        }

        [Fact]
        public async void GetCompanyAsync_DbTransaction()
        {
            _logger.LogInformation("GetCompanyAsync_DbTransaction...");
            var newItem = A.New<Company>();
            newItem.Name = $"{newItem.Name} ({DateTime.Now:yyyyMMddHHmmssfff} - {Guid.NewGuid()})";
            newItem.ObjectDocument = $"{{\"Test\": \"{newItem.Name}\"}}";
            using (var dbTransaction = await DbConnectionFactory.BeginUserTransactionAsync(UserId))
            {
                try
                {
                    newItem = await _dbContext.InsertAsync(dbTransaction, newItem);
                    dbTransaction.Commit();

                    Assert.NotNull(newItem);
                    Assert.NotEqual(0, newItem.CompanyId);
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
                    await _dbContext.GetAsync(dbTransaction.Connection, newItem.CompanyId, dbTransaction);

                Assert.NotNull(result);
                Assert.Equal(newItem.CompanyId, result.CompanyId);
            }
        }

        //[Fact]
        //public async void GetCompanyCountAsync()
        //{
        //    Logger.LogInformation("GetCompanyCountAsync...");
        //    using (var dbConnection = await DbConnectionFactory.OpenUserConnectionAsync(UserId))
        //    {
        //        var result = await _dbContext.GetCompanyCountAsync(dbConnection);
        //        Assert.NotEqual(0, result);
        //    }
        //}

        [Fact]
        public async void InsertCompanyAsync()
        {
            _logger.LogInformation("InsertCompanyAsync...");
            var newItem = A.New<Company>();
            newItem.Name = $"{newItem.Name} ({DateTime.Now:yyyyMMddHHmmssfff} - {Guid.NewGuid()})";
            newItem.ObjectDocument = $"{{\"Test\": \"{newItem.Name}\"}}";
            using (var dbTransaction = await DbConnectionFactory.BeginUserTransactionAsync(UserId))
            {
                try
                {
                    var result = await _dbContext.InsertAsync(dbTransaction, newItem);
                    dbTransaction.Commit();

                    Assert.NotNull(result);
                    Assert.NotEqual(0, result.CompanyId);
                }
                catch
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }
        }

        [Fact]
        public async void UpdateCompanyAsync()
        {
            _logger.LogInformation("UpdateCompanyAsync...");
            var newItem = A.New<Company>();
            newItem.Name = $"{newItem.Name} ({DateTime.Now:yyyyMMddHHmmssfff} - {Guid.NewGuid()})";
            newItem.ObjectDocument = $"{{\"Test\": \"{newItem.Name}\"}}";
            using (var dbTransaction = await DbConnectionFactory.BeginUserTransactionAsync(UserId))
            {
                try
                {
                    newItem = await _dbContext.InsertAsync(dbTransaction, newItem);
                    dbTransaction.Commit();

                    Assert.NotNull(newItem);
                    Assert.NotEqual(0, newItem.CompanyId);
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
                    var itemToUpdate = A.New<Company>();
                    itemToUpdate.CompanyId = newItem.CompanyId;
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
                var updatedItem = await _dbContext.GetAsync(dbConnection, newItem.CompanyId);
                Assert.NotNull(updatedItem);
                Assert.NotEqual(updatedItem.Name, newItem.Name);
            }
        }
    }
}