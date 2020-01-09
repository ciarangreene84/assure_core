using Assure.Core.DataAccessLayer.Interfaces.DbContexts;
using Assure.Core.DataAccessLayer.Interfaces.Models;
using Assure.Core.Shared.Interfaces.Models;
using GenFu;
using Newtonsoft.Json;
using System;
using System.Linq;
using Xunit;


namespace Assure.Core.DataAccessLayer.Tests.Integration.DbContexts
{
    public class AgentsDbContextTests : AbstractDbContextTests<IAgentsDbContext, Agent>
    {
        public AgentsDbContextTests() 
        {

        }

        [Fact]
        public async void GetAgentsAsync()
        {
            using (var dbConnection = await DbConnectionFactory.OpenUserConnectionAsync(UserId))
            {
                var result = await _dbContext.GetAllAsync(dbConnection);
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async void GetAgentsAsync_PageRequest()
        {
            var pageRequest = new PageRequest()
            {
                SortBy = "AgentId",
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
        public async void InsertAgentAsync()
        {
            var newItem = A.New<Agent>();
            using (var dbConnection = await DbConnectionFactory.OpenUserConnectionAsync(UserId))
            {
                var items = (await _dbContext.GetAllAsync(dbConnection)).ToList();
                newItem.Name = $"{newItem.Name} ({items.Count})";
            }
            using (var dbTransaction = await DbConnectionFactory.BeginUserTransactionAsync(UserId))
            {
                try
                {
                    var result = await _dbContext.InsertAsync(dbTransaction, newItem);
                    dbTransaction.Commit();

                    Assert.NotNull(result);
                    Assert.NotEqual(0, result.AgentId);

                }
                catch
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }
        }

        [Fact]
        public async void UpdateAgentAsync()
        {
            var newItem = A.New<Agent>();
            using (var dbConnection = await DbConnectionFactory.OpenUserConnectionAsync(UserId))
            {
                var items = (await _dbContext.GetAllAsync(dbConnection)).ToList();
                newItem.Name = $"{newItem.Name} ({items.Count})";
            }
            using (var dbTransaction = await DbConnectionFactory.BeginUserTransactionAsync(UserId))
            {
                try
                {
                    var insertedItem = await _dbContext.InsertAsync(dbTransaction, newItem);
                    insertedItem.Name = $"{insertedItem.Name} ({DateTime.Now})";
                    Assert.NotNull(insertedItem);
                    Assert.NotEqual(0, insertedItem.AgentId);

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
        public async void DeleteAgentAsync()
        {
            var newItem = A.New<Agent>();
            using (var dbConnection = await DbConnectionFactory.OpenUserConnectionAsync(UserId))
            {
                var items = (await _dbContext.GetAllAsync(dbConnection)).ToList();
                newItem.Name = $"{newItem.Name} ({items.Count})";
                newItem.ObjectDocument = JsonConvert.SerializeObject(new {Identifier = Guid.NewGuid()});
                newItem.ObjectHash = newItem.ObjectDocument.GetHashCode();
            }
            using (var dbTransaction = await DbConnectionFactory.BeginUserTransactionAsync(UserId))
            {
                try
                {
                    var result = await _dbContext.InsertAsync(dbTransaction, newItem);
                    dbTransaction.Commit();

                    Assert.NotNull(result);
                    Assert.NotEqual(0, result.AgentId);

                }
                catch
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }

            Agent existingAgent;
            using (var dbConnection = await DbConnectionFactory.OpenUserConnectionAsync(UserId))
            {
                var pageRequest = new PageRequest()
                {
                    PageIndex = 0,
                    PageSize = 1,
                    SortBy = "AgentId",
                    SortOrder = SortOrders.DESC
                };
                existingAgent = (await _dbContext.GetPageAsync(dbConnection, pageRequest)).Items.First();
            }

            using (var transaction = await DbConnectionFactory.BeginUserTransactionAsync(UserId))
            {
                try
                {
                    await _dbContext.DeleteAsync(transaction, existingAgent);
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
                var result = await _dbContext.GetAsync(dbConnection, existingAgent.AgentId);
                Assert.Null(result);
            }
        }
    }
}
