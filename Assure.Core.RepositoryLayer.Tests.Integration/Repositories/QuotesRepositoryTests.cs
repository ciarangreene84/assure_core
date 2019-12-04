using Assure.Core.RepositoryLayer.Interfaces.Repositories;
using Assure.Core.RepositoryLayer.Tests.Integration.TestModels;
using Assure.Core.Shared.Interfaces.Models;
using GenFu;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq;
using Xunit;


namespace Assure.Core.RepositoryLayer.Tests.Integration.Repositories
{
    public class QuotesCoreRepositoryTests : AbstractCoreRepositoryTests<IQuotesRepository, TestQuote>
    {
        public QuotesCoreRepositoryTests()
        {

        }

        [Fact]
        public async void InsertQuoteAsync()
        {
            var newItem = A.New<TestQuote>();
            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var items = (await _repository.GetAsync<TestQuote>(dbConnection)).ToList();
            }
            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    var result = await _repository.InsertAsync<TestQuote>(dbTransaction, newItem);
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

        [Theory]
        [Trait("Category", "Bulk Load")]
        [InlineData(1000)]
        //[InlineData(100000)]
        public async void InsertQuoteAsync_Multiple(int count)
        {
            _logger.LogInformation($"InsertQuoteAsync_Multiple '{count}'...");
            var newItems = A.ListOf<TestQuote>(count);
            foreach (var newItem in newItems)
                using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
                {
                    try
                    {
                        var result = await _repository.InsertAsync(dbTransaction, newItem);
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
            var newItem = A.New<TestQuote>();
            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var items = (await _repository.GetAsync<TestQuote>(dbConnection)).ToList();
            }

            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    var insertedItem = await _repository.InsertAsync<TestQuote>(dbTransaction, newItem);
                    Assert.NotNull(insertedItem);
                    Assert.NotEqual(0, insertedItem.QuoteId);

                    var result = await _repository.UpdateAsync<TestQuote>(dbTransaction, insertedItem);
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
            var newItem = A.New<TestQuote>();
            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var items = (await _repository.GetAsync<TestQuote>(dbConnection)).ToList();
            }

            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    var insertedItem = await _repository.InsertAsync<TestQuote>(dbTransaction, newItem);
                    Assert.NotNull(insertedItem);
                    Assert.NotEqual(0, insertedItem.QuoteId);

                    var result = await _repository.DeleteAsync<TestQuote>(dbTransaction, insertedItem);
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
        public async void GetQuotesAsync_PageRequest()
        {
            var pageRequest = new PageRequest()
            {
                PageIndex = 50,
                PageSize = 10000,
                SortBy = "QuoteId",
                SortOrder = SortOrders.DESC
            };
            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var result = await _repository.GetAsync<TestQuote>(dbConnection, pageRequest);
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async void GetQuotesAsync()
        {
            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var result = await _repository.GetAsync<TestQuote>(dbConnection);
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async void GetQuoteAsync_UsingConnection()
        {
            var newItem = A.New<TestQuote>();
            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var items = (await _repository.GetAsync<TestQuote>(dbConnection)).ToList();
            }
            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    newItem = await _repository.InsertAsync<TestQuote>(dbTransaction, newItem);
                    dbTransaction.Commit();

                    Assert.NotNull(newItem);
                    Assert.NotEqual(0, newItem.QuoteId);

                }
                catch
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }

            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var result = await _repository.GetAsync<TestQuote>(dbConnection, newItem.QuoteId);
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async void GetQuoteAsync_UsingTransaction()
        {
            var newItem = A.New<TestQuote>();
            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var items = (await _repository.GetAsync<TestQuote>(dbConnection)).ToList();
            }
            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    newItem = await _repository.InsertAsync<TestQuote>(dbTransaction, newItem);
                    dbTransaction.Commit();

                    Assert.NotNull(newItem);
                    Assert.NotEqual(0, newItem.QuoteId);

                }
                catch
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }

            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                var result = await _repository.GetAsync<TestQuote>(dbTransaction, newItem.QuoteId);
                Assert.NotNull(result);
            }
        }
    }
}
