using System;
using System.Data;
using System.Threading.Tasks;

namespace Assure.Core.DataAccessLayer.Interfaces.DbConnections
{
    public interface IDbConnectionFactory
    {
        Task<IDbConnection> OpenConnectionAsync();
        Task<IDbConnection> OpenUserConnectionAsync(Guid userId);
        Task<IDbTransaction> BeginUserTransactionAsync(Guid userId);
    }
}