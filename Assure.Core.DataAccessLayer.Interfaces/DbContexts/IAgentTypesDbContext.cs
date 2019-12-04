using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Assure.Core.DataAccessLayer.Interfaces.Models;

namespace Assure.Core.DataAccessLayer.Interfaces.DbContexts
{
    public interface IAgentTypesDbContext
    {
        Task<IEnumerable<AgentType>> GetAgentTypesAsync(IDbConnection dbConnection);
        Task<int> InsertAgentTypeAsync(IDbTransaction transaction, AgentType agentType);
        Task<bool> UpdateAgentTypeAsync(IDbTransaction transaction, AgentType agentType);
        Task<bool> DeleteAgentTypeAsync(IDbTransaction transaction, AgentType agentType);
    }
}
