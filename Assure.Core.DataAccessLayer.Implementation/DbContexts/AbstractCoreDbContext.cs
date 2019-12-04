using Assure.Core.DataAccessLayer.Implementation.Attributes;
using Assure.Core.DataAccessLayer.Interfaces;
using Assure.Core.DataAccessLayer.Interfaces.Models;
using Assure.Core.Shared.Interfaces.Models;
using AutoMapper;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Assure.Core.DataAccessLayer.Implementation.DbContexts
{
    public abstract class AbstractCoreDbContext<TInterface, TModel> where TInterface : ObjectDocumentContainer where TModel : ObjectDocumentContainer
    {
        protected readonly ILogger Logger;
        protected readonly IMapper Mapper;
        private readonly IPageSortValidator _pageSortValidator;

        protected AbstractCoreDbContext(ILogger logger, IMapper mapper, IPageSortValidator pageSortValidator)
        {
            Logger = logger;
            Mapper = mapper;
            _pageSortValidator = pageSortValidator;
        }

        public async Task<IEnumerable<TInterface>> GetAllAsync(IDbConnection dbConnection, IDbTransaction dbTransaction = null)
        {
            Logger.LogInformation($"Getting all {typeof(TInterface)}...");
            var items = await dbConnection.GetAllAsync<TModel>(transaction: dbTransaction);
            return Mapper.Map<IList<TInterface>>(items.ToList());
        }

        public async Task<PageResponse<TInterface>> GetPageAsync(IDbConnection dbConnection, PageRequest pageRequest)
        {
            Logger.LogInformation($"Getting page '{pageRequest}'...");
            _pageSortValidator.ValidateSortByProperty<TInterface>(pageRequest.SortBy);

            var items = await dbConnection.QueryAsync<TModel>(pageRequest);

            return new PageResponse<TInterface>(pageRequest)
            {
                Items = Mapper.Map<IList<TInterface>>(items)
            };
        }

        public async Task<TInterface> GetAsync(IDbConnection dbConnection, int key, IDbTransaction dbTransaction = null)
        {
            Logger.LogInformation($"Getting {typeof(TInterface)} with Id '{key}'...");
            return Mapper.Map<TInterface>(await dbConnection.GetAsync<TModel>(key, transaction: dbTransaction));
        }

        public Task<int> GetCountAsync(IDbConnection dbConnection)
        {
            Logger.LogInformation($"Getting count for {typeof(TInterface)}...");
            var countScalarValuedFunctionAttribute = (typeof(TModel).GetCustomAttribute(typeof(CountScalarValuedFunctionAttribute)) as CountScalarValuedFunctionAttribute);
            if (null == countScalarValuedFunctionAttribute)
            {
                throw new NotSupportedException($"Class '{typeof(TModel)}' does not have the 'CountScalarValuedFunctionAttribute' set.");
            }
            return dbConnection.QueryScalarValueFunctionAsync<int>(countScalarValuedFunctionAttribute.FunctionName);
        }

        public async Task<TInterface> InsertAsync(IDbTransaction transaction, TInterface objectToInsert)
        {
            Logger.LogInformation($"Inserting '{objectToInsert}'...");
            await transaction.Connection.InsertAsync(Mapper.Map<TModel>(objectToInsert), transaction);
            var insertedBenefit = await transaction.GetObjectDocumentAsync<TModel>(objectToInsert.ObjectHash, objectToInsert.ObjectDocument);
            return Mapper.Map<TInterface>(insertedBenefit);
        }

        public Task<bool> UpdateAsync(IDbTransaction transaction, TInterface agent)
        {
            Logger.LogInformation($"Updating  {typeof(TInterface)} '{agent}'...");
            return transaction.Connection.UpdateAsync(Mapper.Map<TModel>(agent), transaction);
        }

        public Task<bool> DeleteAsync(IDbTransaction transaction, TInterface agent)
        {
            Logger.LogInformation($"Deleting  {typeof(TInterface)} '{agent}'...");
            return transaction.Connection.DeleteAsync(Mapper.Map<TModel>(agent), transaction);
        }
    }
}