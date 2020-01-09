using Assure.Core.RepositoryLayer.Interfaces.Repositories;
using Assure.Core.RepositoryLayer.Tests.Integration.TestModels;
using Assure.Core.Shared.Interfaces.Models;
using GenFu;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;


namespace Assure.Core.RepositoryLayer.Tests.Integration.Repositories
{
    public class PoliciesCoreRepositoryTests : AbstractCoreRepositoryTests<IPoliciesRepository, TestPolicy>
    {
        private readonly ICustomersRepository _customersRepository;

        public PoliciesCoreRepositoryTests()
        {
            _customersRepository = _serviceProvider.GetService<ICustomersRepository>();
        }

        [Fact]
        public async void DeletePolicyAsync()
        {
            _logger.LogInformation("DeletePolicyAsync");
            var newItem = A.New<TestPolicy>();
            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    var insertedItem = await _repository.InsertAsync(dbTransaction, newItem);
                    Assert.NotNull(insertedItem);
                    Assert.NotEqual(0, insertedItem.PolicyId);

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
        public async void GetPoliciesAsync()
        {
            _logger.LogInformation("GetPoliciesAsync");
            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var result = await _repository.GetAsync<TestPolicy>(dbConnection);
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async void GetPoliciesAsync_PageRequest()
        {
            _logger.LogInformation("GetPoliciesAsync_PageRequest");
            var pageRequest = new PageRequest
            {
                PageIndex = 50,
                PageSize = 10000,
                SortBy = "PolicyId",
                SortOrder = SortOrders.DESC
            };
            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var result = await _repository.GetAsync<TestPolicy>(dbConnection, pageRequest);
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async void InsertPolicyAsync()
        {
            _logger.LogInformation("InsertPolicyAsync");
            var newItem = A.New<TestPolicy>();
            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    var result = await _repository.InsertAsync(dbTransaction, newItem);
                    dbTransaction.Commit();

                    Assert.NotNull(result);
                    Assert.NotEqual(0, result.PolicyId);
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
        public async void InsertPolicyAsync_Multiple(int count)
        {
            _logger.LogInformation($"InsertPolicyAsync_Multiple '{count}'...");
            var newItems = A.ListOf<TestPolicy>(count);
            foreach (var newItem in newItems)
                using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
                {
                    try
                    {
                        var result = await _repository.InsertAsync(dbTransaction, newItem);
                        dbTransaction.Commit();

                        Assert.NotNull(result);
                        Assert.NotEqual(0, result.PolicyId);
                    }
                    catch
                    {
                        dbTransaction.Rollback();
                        throw;
                    }
                }
        }

        [Fact]
        public async void UpdatePolicyAsync()
        {
            _logger.LogInformation("UpdatePolicyAsync");
            var newItem = A.New<TestPolicy>();
            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    var insertedItem = await _repository.InsertAsync(dbTransaction, newItem);
                    Assert.NotNull(insertedItem);
                    Assert.NotEqual(0, insertedItem.PolicyId);

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

        [Fact]
        public async void GetCustomerPoliciesAsync()
        {
            _logger.LogInformation("GetCustomerPoliciesAsync...");
            var newPolicy = A.New<TestPolicy>();
            TestPolicy insertedPolicy;
            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    insertedPolicy = await _repository.InsertAsync(dbTransaction, newPolicy);
                    Assert.NotNull(insertedPolicy);
                    Assert.NotEqual(0, insertedPolicy.PolicyId);
                    dbTransaction.Commit();
                }
                catch
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }

            var newCustomer = A.New<TestCustomer>();
            TestCustomer insertedCustomer;
            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    insertedCustomer = await _customersRepository.InsertAsync(dbTransaction, newCustomer);
                    Assert.NotNull(insertedCustomer);
                    Assert.NotEqual(0, insertedCustomer.CustomerId);


                    await _customersRepository.AddCustomerPolicy(dbTransaction, insertedCustomer.CustomerId, insertedPolicy.PolicyId);
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
                var result = await _repository.GetCustomerPoliciesAsync<TestPolicy>(dbConnection, insertedCustomer.CustomerId);
                Assert.NotNull(result);
            }
        }
    }
}