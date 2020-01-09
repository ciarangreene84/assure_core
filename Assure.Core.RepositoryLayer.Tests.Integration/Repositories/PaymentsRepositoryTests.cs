using Assure.Core.RepositoryLayer.Interfaces.Repositories;
using Assure.Core.RepositoryLayer.Tests.Integration.TestModels;
using Assure.Core.Shared.Interfaces.Models;
using GenFu;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Xunit;

namespace Assure.Core.RepositoryLayer.Tests.Integration.Repositories
{
    public class PaymentsCoreRepositoryTests : AbstractCoreRepositoryTests<IPaymentsRepository, TestPayment>
    {
        public PaymentsCoreRepositoryTests()
        {

        }

        [Fact]
        public async void GetPaymentsAsync()
        {
            _logger.LogInformation("GetPaymentsAsync");
            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var result = await _repository.GetAsync<TestPayment>(dbConnection);
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async void GetPaymentsAsync_PageRequest()
        {
            _logger.LogInformation("GetPaymentsAsync_PageRequest...");
            var pageRequest = new PageRequest
            {
                SortBy = "PaymentId",
                SortOrder = SortOrders.DESC,
                PageIndex = 0,
                PageSize = 50
            };

            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var result = await _repository.GetAsync<TestPayment>(dbConnection, pageRequest);
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async void GetPaymentAsync()
        {
            _logger.LogInformation("GetPaymentAsync...");
            var pageRequest = new PageRequest
            {
                SortBy = "PaymentId",
                SortOrder = SortOrders.DESC,
                PageIndex = 0,
                PageSize = 50
            };

            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var result = await _repository.GetAsync<TestPayment>(dbConnection, pageRequest);
                Assert.NotNull(result);

                var first = result.Items.First();
                var firstItem = await _repository.GetAsync<TestPayment>(dbConnection, first.PaymentId);
                Assert.NotNull(firstItem);
            }
        }

        [Fact]
        public async void InsertPaymentAsync()
        {
            _logger.LogInformation("InsertPaymentAsync...");
            var newItem = A.New<TestPayment>();
            newItem.Identifier = Guid.NewGuid().ToString();
            newItem.CurrencyAlpha3 = "EUR";
            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    var result = await _repository.InsertAsync(dbTransaction, newItem);
                    dbTransaction.Commit();

                    Assert.NotNull(result);
                    Assert.NotEqual(0, result.PaymentId);
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
