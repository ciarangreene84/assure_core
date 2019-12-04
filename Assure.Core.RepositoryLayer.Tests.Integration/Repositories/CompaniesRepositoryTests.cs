using System;
using Assure.Core.RepositoryLayer.Interfaces.Repositories;
using Assure.Core.RepositoryLayer.Tests.Integration.TestModels;
using Assure.Core.Shared.Interfaces.Models;
using GenFu;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;


namespace Assure.Core.RepositoryLayer.Tests.Integration.Repositories
{
    public class CompaniesCoreRepositoryTests : AbstractCoreRepositoryTests<ICompaniesRepository, TestCompany>
    {
        public CompaniesCoreRepositoryTests()
        {

        }

        [Theory]
        [InlineData(1000)]
        public async void InsertCompanyAsync_Multiple(int count)
        {
            _logger.LogInformation($"InsertCompanyAsync_Multiple '{count}'...");
            var newItems = A.ListOf<TestCompany>(count);
            foreach (var newItem in newItems)
                using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
                {
                    try
                    {
                        newItem.Name = $"{newItem.Name} ({DateTime.Now:yyyyMMddHHmmssfff} - {Guid.NewGuid()})";
                        var result = await _repository.InsertAsync(dbTransaction, newItem);
                        dbTransaction.Commit();

                        Assert.NotNull(result);
                        Assert.NotEqual(0, result.CompanyId);
                    }
                    catch
                    {
                        dbTransaction.Rollback();
                        throw;
                    }
                }
        }

        [Fact]
        public async void DeleteCompanyAsync()
        {
            _logger.LogInformation("DeleteCompanyAsync");
            var newItem = A.New<TestCompany>();
            newItem.Name = $"{newItem.Name} ({DateTime.Now:yyyyMMddHHmmssfff} - {Guid.NewGuid()})";
            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    newItem = await _repository.InsertAsync(dbTransaction, newItem);
                    dbTransaction.Commit();

                    Assert.NotNull(newItem);
                    Assert.NotEqual(0, newItem.CompanyId);
                }
                catch
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }

            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    var result = await _repository.DeleteAsync(dbTransaction, newItem);
                    Assert.True(result);
                    dbTransaction.Commit();
                }
                catch
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }

            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var deleteItem = await _repository.GetAsync<TestCompany>(dbConnection, newItem.CompanyId);
                Assert.Null(deleteItem);
            }
        }

        [Fact]
        public async void GetCompaniesAsync()
        {
            _logger.LogInformation("GetCompaniesAsync");
            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var result = await _repository.GetAsync<TestCompany>(dbConnection);
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async void GetCompaniesAsync_PageRequest()
        {
            _logger.LogInformation("GetCompaniesAsync_PageRequest");
            var pageRequest = new PageRequest
            {
                PageIndex = 50,
                PageSize = 10000,
                SortBy = "CompanyId",
                SortOrder = SortOrders.DESC
            };
            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var result = await _repository.GetAsync<TestCompany>(dbConnection, pageRequest);
                Assert.NotNull(result);
            }
        }


        [Fact]
        public async void GetCompanyAsync()
        {
            _logger.LogInformation("GetCompanyAsync");
            var newItem = A.New<TestCompany>();
            newItem.Name = $"{newItem.Name} ({DateTime.Now:yyyyMMddHHmmssfff} - {Guid.NewGuid()})";
            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    newItem = await _repository.InsertAsync(dbTransaction, newItem);
                    dbTransaction.Commit();

                    Assert.NotNull(newItem);
                    Assert.NotEqual(0, newItem.CompanyId);
                }
                catch
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }

            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var result = await _repository.GetAsync<TestCompany>(dbConnection, newItem.CompanyId);
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async void GetCompanyCountAsync()
        {
            _logger.LogInformation("GetCompanyCountAsync");
            var newItem = A.New<TestCompany>();
            newItem.Name = $"{newItem.Name} ({DateTime.Now:yyyyMMddHHmmssfff} - {Guid.NewGuid()})";
            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    newItem = await _repository.InsertAsync(dbTransaction, newItem);
                    dbTransaction.Commit();

                    Assert.NotNull(newItem);
                    Assert.NotEqual(0, newItem.CompanyId);
                }
                catch
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }

            //using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            //{
            //    var result = await _repository.GetCompanyCountAsync(dbConnection);
            //    Assert.NotEqual(0, result);
            //}
        }

        [Fact]
        public async void InsertCompanyAsync()
        {
            _logger.LogInformation("InsertCompanyAsync");
            var newItem = A.New<TestCompany>();
            newItem.Name = $"{newItem.Name} ({DateTime.Now:yyyyMMddHHmmssfff} - {Guid.NewGuid()})";
            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    newItem = await _repository.InsertAsync(dbTransaction, newItem);
                    dbTransaction.Commit();

                    Assert.NotNull(newItem);
                    Assert.NotEqual(0, newItem.CompanyId);
                }
                catch
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }
        }

        [Fact]
        public async void UpdateCompanyAsync()
        {
            _logger.LogInformation("UpdateCompanyAsync");
            var newItem = A.New<TestCompany>();
            newItem.Name = $"{newItem.Name} ({DateTime.Now:yyyyMMddHHmmssfff} - {Guid.NewGuid()})";
            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    newItem = await _repository.InsertAsync(dbTransaction, newItem);
                    dbTransaction.Commit();

                    Assert.NotNull(newItem);
                    Assert.NotEqual(0, newItem.CompanyId);
                }
                catch
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }

            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    var itemToUpdate = A.New<TestCompany>();
                    itemToUpdate.CompanyId = newItem.CompanyId;
                    itemToUpdate.Name = $"{newItem.Name} (Update)";
                    var result = await _repository.UpdateAsync(dbTransaction, itemToUpdate);
                    Assert.True(result);
                    dbTransaction.Commit();
                }
                catch
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }

            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var updatedItem = await _repository.GetAsync<TestCompany>(dbConnection, newItem.CompanyId);
                Assert.NotNull(updatedItem);
                Assert.Equal(updatedItem.CompanyId, newItem.CompanyId);
                Assert.NotEqual(updatedItem.Name, newItem.Name);
            }
        }
    }
}