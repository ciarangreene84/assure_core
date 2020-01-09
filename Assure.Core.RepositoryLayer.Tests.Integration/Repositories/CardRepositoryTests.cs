using Assure.Core.RepositoryLayer.Interfaces.Repositories;
using Assure.Core.RepositoryLayer.Tests.Integration.TestModels;
using Assure.Core.Shared.Interfaces.Models;
using GenFu;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using Xunit;


namespace Assure.Core.RepositoryLayer.Tests.Integration.Repositories
{
    public class CardCoreRepositoryTests : AbstractCoreRepositoryTests<ICardsRepository, TestCard>
    {
        public CardCoreRepositoryTests()
        {

        }

        [Theory]
        [Trait("Category", "Bulk Load")]
        [InlineData(1000)]
        //[InlineData(100000)]
        public async Task InsertCardAsync_Multiple(int count)
        {
            _logger.LogInformation($"InsertCardAsync_Multiple '{count}'...");
            var newItems = A.ListOf<TestCard>(count);
            var maxCardNumber = 0;
            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var cards = (await _repository.GetAsync<TestCard>(dbConnection)).ToList();
                if (0 < cards.Count)
                {
                    maxCardNumber = cards.Max(card => card.Number);
                }
            }

            foreach (var newItem in newItems)
                using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
                {
                    try
                    {
                        count++;
                        newItem.Number = maxCardNumber + count;
                        var result = await _repository.InsertAsync(dbTransaction, newItem);
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
        public async void DeleteCardAsync()
        {
            _logger.LogInformation("DeleteCardAsync...");
            var newItem = A.New<TestCard>();
            int maxCardNumber;
            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                maxCardNumber = (await _repository.GetAsync<TestCard>(dbConnection)).Max(card => card.Number);
            }

            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    newItem.Number = maxCardNumber + 1;
                    var insertedItem = await _repository.InsertAsync(dbTransaction, newItem);
                    Assert.NotNull(insertedItem);
                    Assert.NotEqual(0, insertedItem.CardId);

                    var result = await _repository.DeleteAsync(dbTransaction, insertedItem);
                    Assert.True(result);
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
        public async void GetCardAsync_UsingConnection()
        {
            _logger.LogInformation("GetCardAsync_UsingConnection...");
            int itemId;
            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                itemId = (await _repository.GetAsync<TestCard>(dbConnection)).First().CardId;
            }

            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var result = await _repository.GetAsync<TestCard>(dbConnection, itemId);
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async void GetCardAsync_UsingTransaction()
        {
            _logger.LogInformation("GetCardAsync_UsingTransaction...");
            int itemId;
            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                itemId = (await _repository.GetAsync<TestCard>(dbConnection)).First().CardId;
            }

            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                var result = await _repository.GetAsync<TestCard>(dbTransaction, itemId);
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async void GetCardsAsync()
        {
            _logger.LogInformation("GetCardsAsync...");
            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var result = await _repository.GetAsync<TestCard>(dbConnection);
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async void GetCardsAsync_PageRequest()
        {
            _logger.LogInformation("GetCardsAsync_PageRequest...");
            var pageRequest = new PageRequest
            {
                PageIndex = 50,
                PageSize = 10000,
                SortBy = "CardId",
                SortOrder = SortOrders.DESC
            };
            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var result = await _repository.GetAsync<TestCard>(dbConnection, pageRequest);
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async void InsertCardAsync()
        {
            _logger.LogInformation("InsertCardAsync...");
            var newItem = A.New<TestCard>();
            var maxCardNumber = 0;
            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var cards = (await _repository.GetAsync<TestCard>(dbConnection)).ToList();
                if (0 < cards.Count)
                {
                    maxCardNumber = cards.Max(card => card.Number);
                }
            }

            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    newItem.Number = maxCardNumber + 1;
                    var result = await _repository.InsertAsync(dbTransaction, newItem);
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
            _logger.LogInformation("UpdateCardAsync...");
            var newItem = A.New<TestCard>();
            int maxCardNumber;
            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                maxCardNumber = (await _repository.GetAsync<TestCard>(dbConnection)).Max(card => card.Number);
            }

            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    newItem.Number = maxCardNumber + 1;
                    var insertedItem = await _repository.InsertAsync(dbTransaction, newItem);
                    Assert.NotNull(insertedItem);
                    Assert.NotEqual(0, insertedItem.CardId);

                    var result = await _repository.UpdateAsync(dbTransaction, insertedItem);
                    Assert.True(result);
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