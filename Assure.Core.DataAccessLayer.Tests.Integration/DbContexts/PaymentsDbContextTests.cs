using Assure.Core.DataAccessLayer.Interfaces.DbContexts;
using Assure.Core.DataAccessLayer.Interfaces.Models;
using Assure.Core.Shared.Interfaces.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using GenFu;
using Xunit;

namespace Assure.Core.DataAccessLayer.Tests.Integration.DbContexts
{
    public class PaymentsDbContextTests : AbstractDbContextTests<IPaymentsDbContext, Payment>
    {
        public PaymentsDbContextTests()
        {

        }
        
        [Fact]
        public async void GetPaymentsAsync()
        {
            _logger.LogInformation("GetPaymentsAsync...");
            using (var dbConnection = await DbConnectionFactory.OpenUserConnectionAsync(UserId))
            {
                var result = await _dbContext.GetAllAsync(dbConnection);
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

            using (var dbConnection = await DbConnectionFactory.OpenUserConnectionAsync(UserId))
            {
                var result = await _dbContext.GetPageAsync(dbConnection, pageRequest);
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async void GetPaymentAsync()
        {
            var newItem = A.New<Payment>();
            newItem.Identifier = Guid.NewGuid().ToString();
            newItem.CurrencyAlpha3 = "EUR";
            newItem.ObjectDocument = $"{{\"Test\": \"{newItem.Identifier} ({DateTime.Now:yyyyMMddHHmmssfff} - {Guid.NewGuid()})\"}}";
            using (var dbTransaction = await DbConnectionFactory.BeginUserTransactionAsync(UserId))
            {
                try
                {
                    var result = await _dbContext.InsertAsync(dbTransaction, newItem);
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

            _logger.LogInformation("GetPaymentAsync...");
            var pageRequest = new PageRequest
            {
                SortBy = "PaymentId",
                SortOrder = SortOrders.DESC,
                PageIndex = 0,
                PageSize = 50
            };

            using (var dbConnection = await DbConnectionFactory.OpenUserConnectionAsync(UserId))
            {
                var result = await _dbContext.GetPageAsync(dbConnection, pageRequest);
                Assert.NotNull(result);

                var first = result.Items.First();
                var firstItem = await _dbContext.GetAsync(dbConnection, first.PaymentId);
                Assert.NotNull(firstItem);
            }
        }

        [Fact]
        public async void InsertPaymentAsync()
        {
            _logger.LogInformation("InsertPaymentAsync...");
            var newItem = A.New<Payment>();
            newItem.Identifier = Guid.NewGuid().ToString();
            newItem.CurrencyAlpha3 = "EUR";
            newItem.ObjectDocument = $"{{\"Test\": \"{newItem.Identifier} ({DateTime.Now:yyyyMMddHHmmssfff} - {Guid.NewGuid()})\"}}";
            using (var dbTransaction = await DbConnectionFactory.BeginUserTransactionAsync(UserId))
            {
                try
                {
                    var result = await _dbContext.InsertAsync(dbTransaction, newItem);
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