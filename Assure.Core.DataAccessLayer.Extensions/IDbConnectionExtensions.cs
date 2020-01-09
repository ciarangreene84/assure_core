using Assure.Core.Shared.Interfaces.Models;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace System.Data
{
    // ReSharper disable once InconsistentNaming
    public static class IDbConnectionExtensions
    {
        public static Task<IEnumerable<T>> QueryAsync<T>(this IDbConnection connection, PageRequest pageRequest) where T : class
        {
            var tableName = typeof(T).GetDapperTableName();
            var sql = $"SELECT * FROM {tableName} ORDER BY {pageRequest.SortBy} {pageRequest.SortOrder} OFFSET {pageRequest.PageIndex * pageRequest.PageSize} ROWS FETCH NEXT {pageRequest.PageSize} ROWS ONLY;";

            return connection.QueryAsync<T>(sql);
        }

        public static Task<IEnumerable<T>> QueryTableValueFunctionAsync<T>(this IDbConnection dbConnection, string tableValuedFunction, object param = null, IDbTransaction dbTransaction = null)
        {
            var parameters = string.Empty;
            if (null != param)
            {
                parameters = string.Join(", ", param.GetType().GetProperties().Select(property => $"@{property.Name}"));
            }
            var query = $"SELECT * FROM {tableValuedFunction}({parameters});";
            return dbConnection.QueryAsync<T>(query, param, dbTransaction);
        }

        public static Task<T> QuerySingleTableValueFunctionAsync<T>(this IDbConnection dbConnection, string tableValuedFunction, object param = null, IDbTransaction dbTransaction = null)
        {
            var parameters = string.Empty;
            if (null != param)
            {
                parameters = string.Join(", ", param.GetType().GetProperties().Select(property => $"@{property.Name}"));
            }
            var query = $"SELECT * FROM {tableValuedFunction}({parameters});";
            return dbConnection.QuerySingleAsync<T>(query, param, dbTransaction);
        }

        public static Task<T> QueryScalarValueFunctionAsync<T>(this IDbConnection dbConnection, string scalarValuedFunction, object param = null, IDbTransaction dbTransaction = null)
        {
            var parameters = string.Empty;
            if (null != param)
            {
                parameters = string.Join(", ", param.GetType().GetProperties().Select(property => $"@{property.Name}"));
            }
            var query = $"SELECT {scalarValuedFunction}({parameters});";
            return dbConnection.ExecuteScalarAsync<T>(query, param, dbTransaction);
        }
    }
}