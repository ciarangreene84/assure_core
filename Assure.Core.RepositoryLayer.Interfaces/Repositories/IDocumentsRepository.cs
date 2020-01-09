using Assure.Core.RepositoryLayer.Interfaces.Models;
using Assure.Core.Shared.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Assure.Core.RepositoryLayer.Interfaces.Repositories
{
    public interface IDocumentsRepository
    {
        Task<PageResponse<Document>> GetDocumentsAsync(IDbConnection dbConnection, PageRequest pageRequest);
        Task<Document> GetDocumentAsync(IDbConnection dbConnection, Guid documentId);
        Task<Document> InsertDocumentAsync(IDbTransaction transaction, Document document);

        Task<IEnumerable<Document>> GetPolicyDocumentsAsync(IDbConnection dbConnection, int policyId);
        Task<int> AddPolicyDocument(IDbTransaction dbTransaction, int policyId, Guid documentId);
        Task<int> RemovePolicyDocument(IDbTransaction dbTransaction, int policyId, Guid documentId);

        Task<IEnumerable<Document>> GetClaimDocumentsAsync(IDbConnection dbConnection, int claimId);
        Task<int> AddClaimDocument(IDbTransaction dbTransaction, int claimId, Guid documentId);
        Task<int> RemoveClaimDocument(IDbTransaction dbTransaction, int claimId, Guid documentId);
    }
}
