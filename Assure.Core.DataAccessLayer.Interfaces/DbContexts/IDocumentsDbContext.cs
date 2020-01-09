using Assure.Core.DataAccessLayer.Interfaces.Models;
using Assure.Core.Shared.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Assure.Core.DataAccessLayer.Interfaces.DbContexts
{
    public interface IDocumentsDbContext
    {
        Task<PageResponse<Document>> GetDocumentsAsync(IDbConnection dbConnection, PageRequest pageRequest);
        Task<Document> GetDocumentAsync(IDbConnection dbConnection, Guid documentId);
        Task<Document> InsertDocumentAsync(IDbTransaction transaction, Document document);

        Task<IEnumerable<Document>> GetPolicyDocumentsAsync(IDbConnection dbConnection, int policyId);
        Task<int> AddPolicyDocumentAsync(IDbTransaction dbTransaction, int policyId, Guid documentId);
        Task<int> RemovePolicyDocumentAsync(IDbTransaction dbTransaction, int policyId, Guid documentId);

        Task<IEnumerable<Document>> GetClaimDocumentsAsync(IDbConnection dbConnection, int claimId);
        Task<int> AddClaimDocumentAsync(IDbTransaction dbTransaction, int claimId, Guid documentId);
        Task<int> RemoveClaimDocumentAsync(IDbTransaction dbTransaction, int claimId, Guid documentId);
    }
}