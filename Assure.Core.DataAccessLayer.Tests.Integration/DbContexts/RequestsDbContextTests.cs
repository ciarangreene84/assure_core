using Assure.Core.DataAccessLayer.Interfaces.DbContexts;
using Assure.Core.DataAccessLayer.Interfaces.Models;
using Assure.Core.Shared.Interfaces.Models;
using GenFu;
using System;
using System.Linq;
using Xunit;

namespace Assure.Core.DataAccessLayer.Tests.Integration.DbContexts
{
    public class RequestsDbContextTests : AbstractDbContextTests<IRequestsDbContext, Request>
    {
        public RequestsDbContextTests() 
        {

        }

        [Fact]
        public async void GetRequestsAsync()
        {
            using (var dbConnection = await DbConnectionFactory.OpenUserConnectionAsync(UserId))
            {
                var result = await _dbContext.GetAllAsync(dbConnection);
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async void GetRequestsForTypeAsync()
        {
            var newItem = A.New<Request>();
            using (var dbTransaction = await DbConnectionFactory.BeginUserTransactionAsync(UserId))
            {
                try
                {
                    var result = await _dbContext.InsertAsync(dbTransaction, newItem);
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
            //using (var dbConnection = await DbConnectionFactory.OpenUserConnectionAsync(UserId))
            //{

            //    var result = await _dbContext.GetAsync(dbConnection, newItem.Type);
            //    Assert.NotNull(result);
            //}
        }

        [Fact]
        public async void GetRequestsAsync_PageRequest()
        {
            var pageRequest = new PageRequest()
            {
                SortBy = "RequestId",
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
        public async void GetRequestsAsync_PageRequest_SortByType_Filtered()
        {
            var pageRequest = new PageRequest()
            {
                Filter = "denim",
                SortBy = "Type",
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
        public async void InsertRequestAsync()
        {
            var newItem = A.New<Request>();
            using (var dbConnection = await DbConnectionFactory.OpenUserConnectionAsync(UserId))
            {
                var items = (await _dbContext.GetAllAsync(dbConnection)).ToList();
                newItem.Type = $"{newItem.Type} ({items.Count})";
            }
            using (var dbTransaction = await DbConnectionFactory.BeginUserTransactionAsync(UserId))
            {
                try
                {
                    var result = await _dbContext.InsertAsync(dbTransaction, newItem);
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
            var newItem = A.New<Request>();
            using (var dbConnection = await DbConnectionFactory.OpenUserConnectionAsync(UserId))
            {
                var items = (await _dbContext.GetAllAsync(dbConnection)).ToList();
                newItem.Type = $"{newItem.Type} ({items.Count})";
            }
            using (var dbTransaction = await DbConnectionFactory.BeginUserTransactionAsync(UserId))
            {
                try
                {
                    var insertedItem = await _dbContext.InsertAsync(dbTransaction, newItem);
                    insertedItem.Type = $"{insertedItem.Type} ({DateTime.Now})";
                    if (32 < insertedItem.Type.Length) insertedItem.Type = insertedItem.Type.Substring(0, 31);
                    Assert.NotNull(insertedItem);
                    Assert.NotEqual(0, insertedItem.RequestId);

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
    }
}
