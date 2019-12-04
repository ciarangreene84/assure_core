using Assure.Core.DataAccessLayer.Interfaces.DbContexts;
using Assure.Core.RepositoryLayer.Interfaces.Models;
using Assure.Core.RepositoryLayer.Interfaces.Repositories;
using Assure.Core.Shared.Interfaces.Models;
using AutoMapper;
using Boot4ServiceCollection.Attributes;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using DalModels = Assure.Core.DataAccessLayer.Interfaces.Models;

namespace Assure.Core.RepositoryLayer.Implementation.Repositories
{
    [AddScoped(typeof(IDocumentsRepository))]
    public sealed class DocumentsRepository : IDocumentsRepository
    {
        private readonly ILogger<DocumentsRepository> _logger;

        private readonly IDocumentsDbContext _dbContext;
        private readonly IMapper _mapper;

        public DocumentsRepository(ILogger<DocumentsRepository> logger, IDocumentsDbContext dbContext, IMapper mapper)
        {
            _logger = logger;

            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<PageResponse<Document>> GetDocumentsAsync(IDbConnection dbConnection, PageRequest pageRequest)
        {
            _logger.LogInformation($"Getting documents page '{pageRequest}'...");
            var dalObject = await _dbContext.GetDocumentsAsync(dbConnection, pageRequest);
            return _mapper.Map<PageResponse<Document>>(dalObject);
        }

        public async Task<Document> GetDocumentAsync(IDbConnection dbConnection, Guid documentId) 
        {
            _logger.LogInformation($"Getting document with ID '{documentId}'...");
            var dalObject = await _dbContext.GetDocumentAsync(dbConnection, documentId);
            return _mapper.Map<Document>(dalObject);
        }

        public async Task<Document> InsertDocumentAsync(IDbTransaction dbTransaction, Document document)
        {
            _logger.LogInformation($"Inserting document '{document}'...");
            var dalObject = _mapper.Map<DalModels.Document>(document);
            return _mapper.Map<Document>(await _dbContext.InsertDocumentAsync(dbTransaction, dalObject));
        }


        public async Task<IEnumerable<Document>> GetPolicyDocumentsAsync(IDbConnection dbConnection, int policyId)
        {
            _logger.LogInformation($"Getting documents for policy '{policyId}'...");
            var dalObject = await _dbContext.GetPolicyDocumentsAsync(dbConnection, policyId);
            return _mapper.Map<IList<Document>>(dalObject.ToList());
        }
        public Task<int> AddPolicyDocument(IDbTransaction dbTransaction, int policyId, Guid documentId)
        {
            _logger.LogInformation($"Adding document with Id '{documentId}' to policy with Id '{policyId}'...");
            return _dbContext.AddPolicyDocumentAsync(dbTransaction, policyId, documentId);
        }
        public Task<int> RemovePolicyDocument(IDbTransaction dbTransaction, int policyId, Guid documentId)
        {
            _logger.LogInformation($"Removing document with Id '{documentId}' from policy with Id '{policyId}'...");
            return _dbContext.RemovePolicyDocumentAsync(dbTransaction, policyId, documentId);
        }


        public async Task<IEnumerable<Document>> GetClaimDocumentsAsync(IDbConnection dbConnection, int claimId)
        {
            _logger.LogInformation($"Getting documents for claim '{claimId}'...");
            var dalObject = await _dbContext.GetClaimDocumentsAsync(dbConnection, claimId);
            return _mapper.Map<IList<Document>>(dalObject.ToList());
        }
        public Task<int> AddClaimDocument(IDbTransaction dbTransaction, int claimId, Guid documentId)
        {
            _logger.LogInformation($"Adding document with Id '{documentId}' to claim with Id '{claimId}'...");
            return _dbContext.AddClaimDocumentAsync(dbTransaction, claimId, documentId);
        }
        public Task<int> RemoveClaimDocument(IDbTransaction dbTransaction, int claimId, Guid documentId)
        {
            _logger.LogInformation($"Removing document with Id '{documentId}' from claim with Id '{claimId}'...");
            return _dbContext.RemoveClaimDocumentAsync(dbTransaction, claimId, documentId);
        }
    }
}
