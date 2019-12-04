using Assure.Core.DataAccessLayer.Interfaces.DbConnections;
using Boot4ServiceCollection.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Assure.Core.DataAccessLayer.Implementation.DbConnections
{
    [AddSingleton(typeof(IDbConnectionFactory))]
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly ILogger<DbConnectionFactory> _logger;

        private readonly AssureDataAccessLayerSettings _settings;

        public DbConnectionFactory(ILogger<DbConnectionFactory> logger, IOptions<AssureDataAccessLayerSettings> options)
        {
            _logger = logger;
            _settings = options.Value;
        }

        public async Task<IDbConnection> OpenConnectionAsync()
        {
            _logger.LogDebug($"Opening database connection...");
            return await OpenConnectionAsync(_settings.ReadOnlyConnectionString);
        }

        public async Task<IDbConnection> OpenUserConnectionAsync(Guid userId)
        {
            _logger.LogInformation($"Opening database connection for user '{userId}'...");

            var dbConnection = await OpenConnectionAsync(_settings.ReadOnlyConnectionString);
            await SetConnectionUser(userId, dbConnection);

            _logger.LogInformation($"Returning connection connection for user '{userId}'.");
            return dbConnection;
        }

        public async Task<IDbTransaction> BeginUserTransactionAsync(Guid userId)
        {
            _logger.LogInformation($"Beginning database transaction for user '{userId}'...");

            var dbConnection = await OpenConnectionAsync(_settings.WriteConnectionString);
            await SetConnectionUser(userId, dbConnection);
            var transaction = dbConnection.BeginTransaction();

            _logger.LogInformation($"Returning database transaction for user '{userId}'.");
            return transaction;
        }

        private async Task<SqlConnection> OpenConnectionAsync(string connectionString)
        {
            _logger.LogDebug($"Opening database connection '{connectionString}'...");
            var dbConnection = new SqlConnection(connectionString);
            await dbConnection.OpenAsync();
            return dbConnection;
        }

        private async Task SetConnectionUser(Guid userId, SqlConnection dbConnection)
        {
            _logger.LogDebug($"Setting connection user to '{userId}'...");
            var command = dbConnection.CreateCommand();
            command.CommandText = $"EXECUTE AS USER = '{userId}';";
            await command.ExecuteNonQueryAsync();
        }
    }
}
