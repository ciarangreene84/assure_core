using Assure.Core.DataAccessLayer.Interfaces.DbContexts;
using Assure.Core.DataAccessLayer.Interfaces.Models;
using Assure.Core.Shared.Interfaces.Models;
using GenFu;
using Newtonsoft.Json;
using System;
using System.Linq;
using Xunit;


namespace Assure.Core.DataAccessLayer.Tests.Integration.DbContexts
{
    public class CardsDbContextTests : AbstractDbContextTests<ICardsDbContext, Card>
    {
        public CardsDbContextTests() 
        {

        }

        [Fact]
        public async void GetCardsAsync()
        {
            using (var dbConnection = await DbConnectionFactory.OpenUserConnectionAsync(UserId))
            {
                var result = await _dbContext.GetAllAsync(dbConnection);
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async void GetCardsAsync_PageRequest()
        {
            var pageRequest = new PageRequest()
            {
                SortBy = "CardId",
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
        public async void InsertCardAsync()
        {
            var newItem = A.New<Card>();
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
                    Assert.NotEqual(0, result.CardId);

                }
                catch
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }
        }

        [Fact]
        public async void UpdateCardAsync()
        {
            var newItem = A.New<Card>();
            newItem.Number = A.Random.Next(1000000, 9999999);
            newItem.ObjectDocument = JsonConvert.SerializeObject(new { Identifier = Guid.NewGuid() });
            newItem.ObjectHash = newItem.ObjectDocument.GetHashCode();
            using (var dbTransaction = await DbConnectionFactory.BeginUserTransactionAsync(UserId))
            {
                try
                {
                    var insertedItem = await _dbContext.InsertAsync(dbTransaction, newItem);
                    Assert.NotNull(insertedItem);
                    Assert.NotEqual(0, insertedItem.CardId);

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
        public async void DeleteCardAsync()
        {
            var newItem = A.New<Card>();
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
                    Assert.NotEqual(0, result.CardId);

                }
                catch
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }

            Card existingCard;
            using (var dbConnection = await DbConnectionFactory.OpenUserConnectionAsync(UserId))
            {
                var pageRequest = new PageRequest()
                {
                    PageIndex = 0,
                    PageSize = 1,
                    SortBy = "CardId",
                    SortOrder = SortOrders.DESC
                };
                existingCard = (await _dbContext.GetPageAsync(dbConnection, pageRequest)).Items.First();
            }

            using (var transaction = await DbConnectionFactory.BeginUserTransactionAsync(UserId))
            {
                try
                {
                    await _dbContext.DeleteAsync(transaction, existingCard);
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
                var result = await _dbContext.GetAsync(dbConnection, existingCard.CardId);
                Assert.Null(result);
            }
        }
    }
}
