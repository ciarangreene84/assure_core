using Assure.Core.DataAccessLayer.Interfaces.DbContexts;
using Assure.Core.DataAccessLayer.Interfaces.Models;
using Assure.Core.DataAccessLayer.Tests.Integration.Fixtures;
using Assure.Core.Shared.Interfaces.Models;
using GenFu;
using Microsoft.Extensions.Logging;
using System;
using Xunit;


namespace Assure.Core.DataAccessLayer.Tests.Integration.DbContexts
{
    public class ClaimsDbContextTests : AbstractDbContextTests<IClaimsDbContext, Claim>, IClassFixture<PoliciesFixture>
    {
        public ClaimsDbContextTests(PoliciesFixture policiesFixture)
        {
            _policiesFixture = policiesFixture;
        }

        private readonly PoliciesFixture _policiesFixture;

        [Theory]
        [InlineData(1000)]
        public async void InsertClaimAsync_Multiple(int count)
        {
            _logger.LogInformation($"InsertClaimAsync_Multiple '{count}'...");

            var objectHash = count;
            using (var dbConnection = await DbConnectionFactory.OpenUserConnectionAsync(UserId))
            {
                objectHash += await _dbContext.GetCountAsync(dbConnection);
            }

            var newItems = A.ListOf<Claim>(count);
            foreach (var newItem in newItems)
            {
                var policy = _policiesFixture.Items[A.Random.Next(0, _policiesFixture.Items.Count - 1)];
                newItem.PolicyId = policy.PolicyId;
                newItem.ObjectDocument = $"{{\"Test\": \"{newItem.PolicyId} ({DateTime.Now:yyyyMMddHHmmssfff} - {Guid.NewGuid()})\"}}";
                newItem.ObjectHash = objectHash++;
                using (var dbTransaction = await DbConnectionFactory.BeginUserTransactionAsync(UserId))
                {
                    try
                    {
                        var result = await _dbContext.InsertAsync(dbTransaction, newItem);
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
        }

        [Fact]
        public async void DeleteClaimAsync()
        {
            _logger.LogInformation("DeleteClaimAsync...");
            var newItem = A.New<Claim>();
            var policy = _policiesFixture.Items[A.Random.Next(0, _policiesFixture.Items.Count - 1)];
            newItem.PolicyId = policy.PolicyId;
            newItem.ObjectDocument = $"{{\"Test\": \"{newItem.PolicyId} ({DateTime.Now:yyyyMMddHHmmssfff} - {Guid.NewGuid()})\"}}";
            using (var dbTransaction = await DbConnectionFactory.BeginUserTransactionAsync(UserId))
            {
                try
                {
                    newItem = await _dbContext.InsertAsync(dbTransaction, newItem);
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
                var result = await _dbContext.GetAsync(dbConnection, newItem.ClaimId);
                Assert.Null(result);
            }
        }

        [Fact]
        public async void GetClaimsAsync_DbConnection()
        {
            _logger.LogInformation("GetClaimsAsync...");
            using (var dbConnection = await DbConnectionFactory.OpenUserConnectionAsync(UserId))
            {
                var result = await _dbContext.GetAllAsync(dbConnection);
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async void GetClaimsAsync_DbTransaction()
        {
            _logger.LogInformation("GetClaimsAsync...");
            using (var dbTransaction = await DbConnectionFactory.BeginUserTransactionAsync(UserId))
            {
                var result = await _dbContext.GetAllAsync(dbTransaction.Connection, dbTransaction);
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async void GetClaimsAsync_PageRequest()
        {
            _logger.LogInformation("GetClaimsAsync_PageRequest...");
            var pageRequest = new PageRequest
            {
                SortBy = "ClaimId",
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
        public async void GetClaimAsync_DbConnection()
        {
            _logger.LogInformation("GetClaimAsync_DbConnection...");
            var newItem = A.New<Claim>();
            var policy = _policiesFixture.Items[A.Random.Next(0, _policiesFixture.Items.Count - 1)];
            newItem.PolicyId = policy.PolicyId;
            newItem.ObjectDocument = $"{{\"Test\": \"{newItem.PolicyId} ({DateTime.Now:yyyyMMddHHmmssfff} - {Guid.NewGuid()})\"}}";
            using (var dbTransaction = await DbConnectionFactory.BeginUserTransactionAsync(UserId))
            {
                try
                {
                    newItem = await _dbContext.InsertAsync(dbTransaction, newItem);
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

            using (var dbConnection = await DbConnectionFactory.OpenUserConnectionAsync(UserId))
            {
                var result = await _dbContext.GetAsync(dbConnection, newItem.ClaimId);

                Assert.NotNull(result);
                Assert.Equal(newItem.ClaimId, result.ClaimId);
            }
        }

        [Fact]
        public async void GetClaimAsync_DbTransaction()
        {
            _logger.LogInformation("GetClaimAsync_DbTransaction...");
            var newItem = A.New<Claim>();
            var policy = _policiesFixture.Items[A.Random.Next(0, _policiesFixture.Items.Count - 1)];
            newItem.PolicyId = policy.PolicyId;
            newItem.ObjectDocument = $"{{\"Test\": \"{newItem.PolicyId} ({DateTime.Now:yyyyMMddHHmmssfff} - {Guid.NewGuid()})\"}}";
            using (var dbTransaction = await DbConnectionFactory.BeginUserTransactionAsync(UserId))
            {
                try
                {
                    newItem = await _dbContext.InsertAsync(dbTransaction, newItem);
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

            using (var dbTransaction = await DbConnectionFactory.BeginUserTransactionAsync(UserId))
            {
                var result =
                    await _dbContext.GetAsync(dbTransaction.Connection, newItem.ClaimId, dbTransaction);

                Assert.NotNull(result);
                Assert.Equal(newItem.ClaimId, result.ClaimId);
            }
        }

        //[Fact]
        //public async void GetClaimCountAsync()
        //{
        //    Logger.LogInformation("GetClaimCountAsync...");
        //    using (var dbConnection = await DbConnectionFactory.OpenUserConnectionAsync(UserId))
        //    {
        //        var result = await _dbContext.GetClaimCountAsync(dbConnection);
        //        Assert.NotEqual(0, result);
        //    }
        //}

        [Fact]
        public async void InsertClaimAsync()
        {
            _logger.LogInformation("InsertClaimAsync...");
            var newItem = A.New<Claim>();
            var policy = _policiesFixture.Items[A.Random.Next(0, _policiesFixture.Items.Count - 1)];
            newItem.PolicyId = policy.PolicyId;
            newItem.ObjectDocument = $"{{\"Test\": \"{newItem.PolicyId} ({DateTime.Now:yyyyMMddHHmmssfff} - {Guid.NewGuid()})\"}}";
            using (var dbTransaction = await DbConnectionFactory.BeginUserTransactionAsync(UserId))
            {
                try
                {
                    var result = await _dbContext.InsertAsync(dbTransaction, newItem);
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

        [Fact]
        public async void UpdateClaimAsync()
        {
            _logger.LogInformation("UpdateClaimAsync...");
            var newItem = A.New<Claim>();
            var policy = _policiesFixture.Items[A.Random.Next(0, _policiesFixture.Items.Count - 1)];
            newItem.PolicyId = policy.PolicyId;
            newItem.ObjectDocument = $"{{\"Test\": \"{newItem.PolicyId} ({DateTime.Now:yyyyMMddHHmmssfff} - {Guid.NewGuid()})\"}}";
            using (var dbTransaction = await DbConnectionFactory.BeginUserTransactionAsync(UserId))
            {
                try
                {
                    newItem = await _dbContext.InsertAsync(dbTransaction, newItem);
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

            using (var dbTransaction = await DbConnectionFactory.BeginUserTransactionAsync(UserId))
            {
                try
                {
                    var itemToUpdate = A.New<Claim>();
                    itemToUpdate.ClaimId = newItem.ClaimId;
                    newItem.ObjectDocument = $"{{\"Test\": \"{newItem.PolicyId} ({DateTime.Now:yyyyMMddHHmmssfff} - {Guid.NewGuid()})\"}}";
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
                var updatedItem = await _dbContext.GetAsync(dbConnection, newItem.ClaimId);
                Assert.NotNull(updatedItem);
                Assert.NotEqual(updatedItem.ObjectDocument, newItem.ObjectDocument);
            }
        }
    }
}