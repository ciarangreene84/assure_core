using Assure.Core.RepositoryLayer.Interfaces.Models;
using Assure.Core.RepositoryLayer.Interfaces.Repositories;
using GenFu;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;


namespace Assure.Core.RepositoryLayer.Tests.Integration.Repositories
{
    public sealed class ProductsRepositoryTests : AbstractConfigurationRepositoryTests<IProductsRepository, TestProduct>
    {
        [Fact]
        public async Task InsertProductAsync()
        {
            var newItem = A.New<TestProduct>();
            var items = (await _repository.GetAsync<TestProduct>()).ToList();
            newItem.ProductId = items.Max(product => product.ProductId) + 1;
            newItem.Name = $"{newItem.Name} ({items.Count})";

            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    var result = await _repository.InsertAsync<TestProduct>(dbTransaction, newItem);
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
            var newItem = A.New<TestProduct>();
            var items = (await _repository.GetAsync<TestProduct>()).ToList();
            newItem.ProductId = items.Max(product => product.ProductId) + 1;
            newItem.Name = $"{newItem.Name} ({items.Count})";

            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    var insertedItem = await _repository.InsertAsync<TestProduct>(dbTransaction, newItem);
                    Assert.NotNull(insertedItem);
                    Assert.NotEqual(0, insertedItem.ProductId);

                    insertedItem.Name = $"{newItem.Name} (Updated: {DateTime.Now})";
                    var result = await _repository.UpdateAsync<TestProduct>(dbTransaction, insertedItem);
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
            var newItem = A.New<TestProduct>();
            var items = (await _repository.GetAsync<TestProduct>()).ToList();
            newItem.ProductId = items.Max(product => product.ProductId) + 1;
            newItem.Name = $"{newItem.Name} ({items.Count})";

            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    var insertedItem = await _repository.InsertAsync<TestProduct>(dbTransaction, newItem);
                    Assert.NotNull(insertedItem);
                    Assert.NotEqual(0, insertedItem.ProductId);

                    var result = await _repository.DeleteAsync<TestProduct>(dbTransaction, insertedItem);
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
        public async void GetProductsAsync_NoConnection()
        {
            var result = (await _repository.GetAsync<TestProduct>()).ToList();
            Assert.NotNull(result);

            result = (await _repository.GetAsync<TestProduct>()).ToList();
            result = (await _repository.GetAsync<TestProduct>()).ToList();
            result = (await _repository.GetAsync<TestProduct>()).ToList();
            result = (await _repository.GetAsync<TestProduct>()).ToList();
            result = (await _repository.GetAsync<TestProduct>()).ToList();
            result = (await _repository.GetAsync<TestProduct>()).ToList();
            result = (await _repository.GetAsync<TestProduct>()).ToList();
            result = (await _repository.GetAsync<TestProduct>()).ToList();
            result = (await _repository.GetAsync<TestProduct>()).ToList();
            result = (await _repository.GetAsync<TestProduct>()).ToList();
        }

    }

    public class TestProduct : Product
    {
        public string Property00 { get; set; }
        public string Property01 { get; set; }
        public string Property02 { get; set; }
        public string Property03 { get; set; }
        public string Property04 { get; set; }
        public string Property05 { get; set; }
        public string Property06 { get; set; }
        public string Property07 { get; set; }
        public string Property08 { get; set; }
        public string Property09 { get; set; }

        public int Property10 { get; set; }
        public int Property11 { get; set; }
        public int Property12 { get; set; }
        public int Property13 { get; set; }
        public int Property14 { get; set; }
        public int Property15 { get; set; }
        public int Property16 { get; set; }
        public int Property17 { get; set; }
        public int Property18 { get; set; }
        public int Property19 { get; set; }

        public decimal Property20 { get; set; }
        public decimal Property21 { get; set; }
        public decimal Property22 { get; set; }
        public decimal Property23 { get; set; }
        public decimal Property24 { get; set; }
        public decimal Property25 { get; set; }
        public decimal Property26 { get; set; }
        public decimal Property27 { get; set; }
        public decimal Property28 { get; set; }
        public decimal Property29 { get; set; }

        public DateTime Property30 { get; set; }
        public DateTime Property31 { get; set; }
        public DateTime Property32 { get; set; }
        public DateTime Property33 { get; set; }
        public DateTime Property34 { get; set; }
        public DateTime Property35 { get; set; }
        public DateTime Property36 { get; set; }
        public DateTime Property37 { get; set; }
        public DateTime Property38 { get; set; }
        public DateTime Property39 { get; set; }

        public string Property40 { get; set; }
        public string Property41 { get; set; }
        public string Property42 { get; set; }
        public string Property43 { get; set; }
        public string Property44 { get; set; }
        public string Property45 { get; set; }
        public string Property46 { get; set; }
        public string Property47 { get; set; }
        public string Property48 { get; set; }
        public string Property49 { get; set; }

        public string Property50 { get; set; }
        public string Property51 { get; set; }
        public string Property52 { get; set; }
        public string Property53 { get; set; }
        public string Property54 { get; set; }
        public string Property55 { get; set; }
        public string Property56 { get; set; }
        public string Property57 { get; set; }
        public string Property58 { get; set; }
        public string Property59 { get; set; }
    }
}
