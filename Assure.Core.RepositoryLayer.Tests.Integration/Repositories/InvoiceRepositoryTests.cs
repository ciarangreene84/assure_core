using Assure.Core.RepositoryLayer.Interfaces.Repositories;
using Assure.Core.RepositoryLayer.Tests.Integration.TestModels;
using Assure.Core.Shared.Interfaces.Models;
using GenFu;
using System;
using System.Linq;
using Xunit;


namespace Assure.Core.RepositoryLayer.Tests.Integration.Repositories
{
    public class InvoiceCoreRepositoryTests : AbstractCoreRepositoryTests<IInvoiceRepository, TestInvoice>
    {
        public InvoiceCoreRepositoryTests()
        {

        }

        [Fact]
        public async void InsertInvoiceAsync()
        {
            var newItem = A.New<TestInvoice>();
            newItem.Identifier = Guid.NewGuid().ToString();
            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    var result = await _repository.InsertAsync<TestInvoice>(dbTransaction, newItem);
                    dbTransaction.Commit();

                    Assert.NotNull(result);
                    Assert.NotEqual(0, result.InvoiceId);

                }
                catch
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }
        }

        [Fact]
        public async void UpdateInvoiceAsync()
        {
            var newItem = A.New<TestInvoice>();
            newItem.Identifier = Guid.NewGuid().ToString();
            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var items = (await _repository.GetAsync<TestInvoice>(dbConnection)).ToList();
            }

            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    var insertedItem = await _repository.InsertAsync<TestInvoice>(dbTransaction, newItem);
                    Assert.NotNull(insertedItem);
                    Assert.NotEqual(0, insertedItem.InvoiceId);

                    var result = await _repository.UpdateAsync<TestInvoice>(dbTransaction, insertedItem);
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
        public async void DeleteInvoiceAsync()
        {
            var newItem = A.New<TestInvoice>();
            newItem.Identifier = Guid.NewGuid().ToString();
            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var items = (await _repository.GetAsync<TestInvoice>(dbConnection)).ToList();
            }

            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    var insertedItem = await _repository.InsertAsync<TestInvoice>(dbTransaction, newItem);
                    Assert.NotNull(insertedItem);
                    Assert.NotEqual(0, insertedItem.InvoiceId);

                    var result = await _repository.DeleteAsync<TestInvoice>(dbTransaction, insertedItem);
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
        public async void GetInvoicesAsync_PageRequest()
        {
            var pageRequest = new PageRequest()
            {
                PageIndex = 50,
                PageSize = 10000,
                SortBy = "InvoiceId",
                SortOrder = SortOrders.DESC
            };
            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var result = await _repository.GetAsync<TestInvoice>(dbConnection, pageRequest);
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async void GetInvoicesAsync()
        {
            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var result = await _repository.GetAsync<TestInvoice>(dbConnection);
                Assert.NotNull(result);
            }
        }
    }


}
