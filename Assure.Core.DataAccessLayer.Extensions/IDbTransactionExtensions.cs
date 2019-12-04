using Assure.Core.DataAccessLayer.Interfaces.Models;
using Dapper;
using System.Linq;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace System.Data
{
    // ReSharper disable once InconsistentNaming
    public static class IDbTransactionExtensions
    {
        public static Task<int> ExecuteStoredProcedureAsync(this IDbTransaction dbTransaction, string storedProcedure, object param)
        {
            var parameters = string.Join(", ", param.GetType().GetProperties().Select(property => $"@{property.Name}"));
            return dbTransaction.Connection.ExecuteAsync($"{storedProcedure} {parameters};", param, dbTransaction);
        }

        public static Task<T> GetObjectDocumentAsync<T>(this IDbTransaction transaction, int ObjectHash, string ObjectDocument) where T : ObjectDocumentContainer
        {
            var tableName = typeof(T).GetDapperTableName();
            var sql = $"SELECT * FROM {tableName} WHERE ObjectHash = @ObjectHash AND ObjectDocument = @ObjectDocument;";
            return transaction.Connection.QuerySingleAsync<T>(sql, new { ObjectHash, ObjectDocument }, transaction);
        }
    }
}
