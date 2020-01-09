using Assure.Core.RepositoryLayer.Interfaces.Repositories;
using Assure.Core.RepositoryLayer.Tests.Integration.Fixtures;
using Assure.Core.RepositoryLayer.Tests.Integration.TestModels;
using Assure.Core.Shared.Interfaces.Models;
using GenFu;
using Microsoft.Extensions.Logging;
using System;
using Xunit;

namespace Assure.Core.RepositoryLayer.Tests.Integration.Repositories
{
    public class ClaimsCoreRepositoryTests : AbstractCoreRepositoryTests<IClaimsRepository, TestClaim>, IClassFixture<PoliciesFixture>
    {
        private readonly PoliciesFixture _policiesFixture;

        public ClaimsCoreRepositoryTests(PoliciesFixture policiesFixture)
        {
            _policiesFixture = policiesFixture;
        }

        [Theory]
        [InlineData(1000)]
        //[InlineData(100000)]
        public async void InsertClaimAsync_Multiple(int count)
        {
            _logger.LogInformation($"InsertClaimAsync_Multiple '{count}'...");
            var newItems = A.ListOf<TestClaim>(count);
            foreach (var newItem in newItems)
                using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
                {
                    try
                    {
                        var policy = _policiesFixture.Items[A.Random.Next(0, _policiesFixture.Items.Count - 1)];
                        newItem.PolicyId = policy.PolicyId;
                        var result = await _repository.InsertAsync(dbTransaction, newItem);
                        dbTransaction.Commit();

                        Assert.NotNull(result);
                        Assert.NotEqual(0, result.ClaimId);
                    }
                    catch
                    {
                        dbTransaction.Rollback();
                        throw;
                    }
                }
        }

        [Fact]
        public async void DeleteClaimAsync()
        {
            _logger.LogInformation("DeleteClaimAsync");
            var newItem = A.New<TestClaim>();
            var policy = A.Random.Next(_policiesFixture.Items);
            newItem.PolicyId = policy.PolicyId;
            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    newItem = await _repository.InsertAsync(dbTransaction, newItem);
                    dbTransaction.Commit();

                    Assert.NotNull(newItem);
                    Assert.NotEqual(0, newItem.ClaimId);
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
                var deleteItem = await _repository.GetAsync<TestClaim>(dbConnection, newItem.ClaimId);
                Assert.Null(deleteItem);
            }
        }

        [Fact]
        public async void GetClaimsAsync()
        {
            _logger.LogInformation("GetClaimsAsync");
            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var result = await _repository.GetAsync<TestClaim>(dbConnection);
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async void GetClaimsAsync_PageRequest()
        {
            _logger.LogInformation("GetClaimsAsync_PageRequest");
            var pageRequest = new PageRequest
            {
                PageIndex = 50,
                PageSize = 10000,
                SortBy = "ClaimId",
                SortOrder = SortOrders.DESC
            };
            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var result = await _repository.GetAsync<TestClaim>(dbConnection, pageRequest);
                Assert.NotNull(result);
            }
        }


        [Fact]
        public async void GetClaimAsync()
        {
            _logger.LogInformation("GetClaimAsync");
            var newItem = A.New<TestClaim>();
            var policy = _policiesFixture.Items[A.Random.Next(0, _policiesFixture.Items.Count - 1)];
            newItem.PolicyId = policy.PolicyId;
            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    newItem = await _repository.InsertAsync(dbTransaction, newItem);
                    dbTransaction.Commit();

                    Assert.NotNull(newItem);
                    Assert.NotEqual(0, newItem.ClaimId);
                }
                catch
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }

            using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            {
                var result = _repository.GetAsync<TestClaim>(dbConnection, newItem.ClaimId);
                result.Wait();
                Assert.NotNull(result.Result);
            }
        }

        [Fact]
        public async void GetClaimCountAsync()
        {
            _logger.LogInformation("GetClaimCountAsync");
            var newItem = A.New<TestClaim>();
            var policy = _policiesFixture.Items[A.Random.Next(0, _policiesFixture.Items.Count - 1)];
            newItem.PolicyId = policy.PolicyId;
            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    newItem = await _repository.InsertAsync(dbTransaction, newItem);
                    dbTransaction.Commit();

                    Assert.NotNull(newItem);
                    Assert.NotEqual(0, newItem.ClaimId);
                }
                catch
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }

            //using (var dbConnection = await _dbConnectionFactory.OpenUserConnectionAsync(_userId))
            //{
            //    var result = await _repository.GetClaimCountAsync(dbConnection);
            //    Assert.NotEqual(0, result);
            //}
        }

        [Fact]
        public async void InsertClaimAsync()
        {
            _logger.LogInformation("InsertClaimAsync");
            var newItem = A.New<TestClaim>();
            var policy = _policiesFixture.Items[A.Random.Next(0, _policiesFixture.Items.Count - 1)];
            newItem.PolicyId = policy.PolicyId;
            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    newItem = await _repository.InsertAsync(dbTransaction, newItem);
                    dbTransaction.Commit();

                    Assert.NotNull(newItem);
                    Assert.NotEqual(0, newItem.ClaimId);
                }
                catch
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }
        }

        [Fact]
        public async void UpdateClaimAsync()
        {
            _logger.LogInformation("UpdateClaimAsync");
            var newItem = A.New<TestClaim>();
            var policy = _policiesFixture.Items[A.Random.Next(0, _policiesFixture.Items.Count - 1)];
            newItem.PolicyId = policy.PolicyId;
            using (var dbTransaction = await _dbConnectionFactory.BeginUserTransactionAsync(_userId))
            {
                try
                {
                    newItem = await _repository.InsertAsync(dbTransaction, newItem);
                    dbTransaction.Commit();

                    Assert.NotNull(newItem);
                    Assert.NotEqual(0, newItem.ClaimId);
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
                    var itemToUpdate = A.New<TestClaim>();
                    itemToUpdate.ClaimId = newItem.ClaimId;
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
                var updatedItem = await _repository.GetAsync<TestClaim>(dbConnection, newItem.ClaimId);
                Assert.NotNull(updatedItem);
                Assert.Equal(updatedItem.ClaimId, newItem.ClaimId);
            }
        }
    }
}