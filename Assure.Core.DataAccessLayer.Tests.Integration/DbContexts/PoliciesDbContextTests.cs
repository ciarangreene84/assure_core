using Assure.Core.DataAccessLayer.Interfaces.DbContexts;
using Assure.Core.DataAccessLayer.Interfaces.Models;
using Assure.Core.Shared.Interfaces.Models;
using GenFu;
using System.Linq;
using Xunit;


namespace Assure.Core.DataAccessLayer.Tests.Integration.DbContexts
{
    public class PoliciesDbContextTests : AbstractDbContextTests<IPoliciesDbContext, Policy>
    {
        public PoliciesDbContextTests() 
        {

        }

        [Fact]
        public async void GetPoliciesAsync()
        {
            using (var dbConnection = await DbConnectionFactory.OpenUserConnectionAsync(UserId))
            {
                var result = await _dbContext.GetAllAsync(dbConnection);
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async void GetPoliciesAsync_PageRequest()
        {
            var pageRequest = new PageRequest()
            {
                SortBy = "PolicyId",
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
        public async void InsertPolicyAsync()
        {
            var newItem = A.New<Policy>();
            using (var dbConnection = await DbConnectionFactory.OpenUserConnectionAsync(UserId))
            {
                var items = (await _dbContext.GetAllAsync(dbConnection)).ToList();
            }
            using (var dbTransaction = await DbConnectionFactory.BeginUserTransactionAsync(UserId))
            {
                try
                {
                    var result = await _dbContext.InsertAsync(dbTransaction, newItem);
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

        [Fact]
        public async void UpdatePolicyAsync()
        {
            var newItem = A.New<Policy>();
            using (var dbConnection = await DbConnectionFactory.OpenUserConnectionAsync(UserId))
            {
                var items = (await _dbContext.GetAllAsync(dbConnection)).ToList();
            }
            using (var dbTransaction = await DbConnectionFactory.BeginUserTransactionAsync(UserId))
            {
                try
                {
                    var insertedItem = await _dbContext.InsertAsync(dbTransaction, newItem);
                    Assert.NotNull(insertedItem);
                    Assert.NotEqual(0, insertedItem.PolicyId);

                    var result = await _dbContext.UpdateAsync(dbTransaction, insertedItem);
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
        public async void DeletePolicyAsync()
        {
            var newItem = A.New<Policy>();
            using (var dbConnection = await DbConnectionFactory.OpenUserConnectionAsync(UserId))
            {
                var items = (await _dbContext.GetAllAsync(dbConnection)).ToList();
            }
            using (var dbTransaction = await DbConnectionFactory.BeginUserTransactionAsync(UserId))
            {
                try
                {
                    var result = await _dbContext.InsertAsync(dbTransaction, newItem);
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

            Policy existingPolicy;
            using (var dbConnection = await DbConnectionFactory.OpenUserConnectionAsync(UserId))
            {
                var pageRequest = new PageRequest()
                {
                    PageIndex = 0,
                    PageSize = 1,
                    SortBy = "PolicyId",
                    SortOrder = SortOrders.DESC
                };
                existingPolicy = (await _dbContext.GetPageAsync(dbConnection, pageRequest)).Items.First();
            }

            using (var transaction = await DbConnectionFactory.BeginUserTransactionAsync(UserId))
            {
                try
                {
                    await _dbContext.DeleteAsync(transaction, existingPolicy);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }

            using (var dbConnection = await DbConnectionFactory.OpenUserConnectionAsync(UserId))
            {
                var result = await _dbContext.GetAsync(dbConnection, existingPolicy.PolicyId);
                Assert.Null(result);
            }
        }
    }
}
