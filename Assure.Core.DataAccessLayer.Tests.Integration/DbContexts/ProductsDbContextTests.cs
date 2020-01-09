using Assure.Core.DataAccessLayer.Interfaces.DbContexts;
using Assure.Core.DataAccessLayer.Interfaces.Models;
using Assure.Core.Shared.Interfaces.Models;
using GenFu;
using System;
using System.Linq;
using Xunit;


namespace Assure.Core.DataAccessLayer.Tests.Integration.DbContexts
{
    public class ProductsDbContextTests : AbstractDbContextTests<IProductsDbContext, Product>
    {
        public ProductsDbContextTests() 
        {

        }

        [Fact]
        public async void GetProductsAsync()
        {
            using (var dbConnection = await DbConnectionFactory.OpenUserConnectionAsync(UserId))
            {
                var result = await _dbContext.GetAllAsync(dbConnection);
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async void GetProductsAsync_PageRequest()
        {
            var pageRequest = new PageRequest()
            {
                SortBy = "ProductId",
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
        public async void InsertProductAsync()
        {
            var newItem = A.New<Product>();
            using (var dbConnection = await DbConnectionFactory.OpenUserConnectionAsync(UserId))
            {
                var items = (await _dbContext.GetAllAsync(dbConnection)).ToList();
                newItem.ProductId = items.Max(product => product.ProductId) + 1;
                newItem.Name = $"{newItem.Name} ({items.Count})";
            }
            using (var dbTransaction = await DbConnectionFactory.BeginUserTransactionAsync(UserId))
            {
                try
                {
                    var result = await _dbContext.InsertAsync(dbTransaction, newItem);
                    dbTransaction.Commit();

                    Assert.NotNull(result);
                    Assert.NotEqual(0, result.ProductId);

                }
                catch
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }
        }

        [Fact]
        public async void UpdateProductAsync()
        {
            var newItem = A.New<Product>();
            using (var dbConnection = await DbConnectionFactory.OpenUserConnectionAsync(UserId))
            {
                var items = (await _dbContext.GetAllAsync(dbConnection)).ToList();
                newItem.ProductId = items.Max(product => product.ProductId) + 1;
                newItem.Name = $"{newItem.Name} ({items.Count})";
            }
            using (var dbTransaction = await DbConnectionFactory.BeginUserTransactionAsync(UserId))
            {
                try
                {
                    var insertedItem = await _dbContext.InsertAsync(dbTransaction, newItem);
                    insertedItem.Name = $"{insertedItem.Name} ({DateTime.Now})";
                    Assert.NotNull(insertedItem);
                    Assert.NotEqual(0, insertedItem.ProductId);

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
        public async void DeleteProductAsync()
        {
            var newItem = A.New<Product>();
            using (var dbConnection = await DbConnectionFactory.OpenUserConnectionAsync(UserId))
            {
                var items = (await _dbContext.GetAllAsync(dbConnection)).ToList();
                newItem.ProductId = items.Max(product => product.ProductId) + 1;
                newItem.Name = $"{newItem.Name} ({items.Count})";
            }

            Product existingProduct;
            using (var dbConnection = await DbConnectionFactory.OpenUserConnectionAsync(UserId))
            {
                var pageRequest = new PageRequest()
                {
                    PageIndex = 0,
                    PageSize = 1,
                    SortBy = "ProductId",
                    SortOrder = SortOrders.DESC
                };
                existingProduct = (await _dbContext.GetPageAsync(dbConnection, pageRequest)).Items.First();
            }

            using (var transaction = await DbConnectionFactory.BeginUserTransactionAsync(UserId))
            {
                try
                {
                    await _dbContext.DeleteAsync(transaction, existingProduct);
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
                var result = await _dbContext.GetAsync(dbConnection, existingProduct.ProductId);
                Assert.Null(result);
            }
        }
    }
}
