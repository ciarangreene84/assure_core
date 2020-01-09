using Assure.Core.DataAccessLayer.Interfaces.DbContexts;
using Assure.Core.DataAccessLayer.Interfaces.Models;
using Assure.Core.Shared.Interfaces.Models;
using GenFu;
using System.Linq;
using Xunit;

namespace Assure.Core.DataAccessLayer.Tests.Integration.DbContexts
{
    public class QuotesDbContextTests : AbstractDbContextTests<IQuotesDbContext, Quote>
    {
        public QuotesDbContextTests() 
        {

        }

        [Fact]
        public async void GetQuotesAsync()
        {
            using (var dbConnection = await DbConnectionFactory.OpenUserConnectionAsync(UserId))
            {
                var result = await _dbContext.GetAllAsync(dbConnection);
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async void GetQuotesAsync_PageRequest()
        {
            var pageRequest = new PageRequest()
            {
                SortBy = "QuoteId",
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
        public async void InsertQuoteAsync()
        {
            var newItem = A.New<Quote>();
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
                    Assert.NotEqual(0, result.QuoteId);

                }
                catch
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }
        }

        [Fact]
        public async void UpdateQuoteAsync()
        {
            var newItem = A.New<Quote>();
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
                    Assert.NotEqual(0, insertedItem.QuoteId);

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
        public async void DeleteQuoteAsync()
        {
            var newItem = A.New<Quote>();
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
                    Assert.NotEqual(0, result.QuoteId);

                }
                catch
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }
            
            Quote existingQuote;
            using (var dbConnection = await DbConnectionFactory.OpenUserConnectionAsync(UserId))
            {
                var pageRequest = new PageRequest()
                {
                    PageIndex = 0,
                    PageSize = 1,
                    SortBy = "QuoteId",
                    SortOrder = SortOrders.DESC
                };
                existingQuote = (await _dbContext.GetPageAsync(dbConnection, pageRequest)).Items.First();
            }

            using (var transaction = await DbConnectionFactory.BeginUserTransactionAsync(UserId))
            {
                try
                {
                    await _dbContext.DeleteAsync(transaction, existingQuote);
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
                var result = await _dbContext.GetAsync(dbConnection, existingQuote.QuoteId);
                Assert.Null(result);
            }
        }
    }
}
