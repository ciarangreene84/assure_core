using Assure.Core.DataAccessLayer.Interfaces.DbContexts;
using Assure.Core.DataAccessLayer.Interfaces.Models;
using Assure.Core.Shared.Interfaces.Models;
using AutoMapper;
using Boot4ServiceCollection.Attributes;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Assure.Core.DataAccessLayer.Interfaces;

namespace Assure.Core.DataAccessLayer.Implementation.DbContexts
{
    [AddScoped(typeof(IDocumentsDbContext))]
    public sealed class DocumentsDbContext : IDocumentsDbContext
    {
        private readonly ILogger<DocumentsDbContext> _logger;
        private readonly IMapper _mapper;
        private readonly IPageSortValidator _pageSortValidator;

        public DocumentsDbContext(ILogger<DocumentsDbContext> logger, IMapper mapper, IPageSortValidator pageSortValidator)
        {
            _logger = logger;
            _mapper = mapper;
            _pageSortValidator = pageSortValidator;
        }

        public async Task<PageResponse<Document>> GetDocumentsAsync(IDbConnection dbConnection, PageRequest pageRequest)
        {
            _logger.LogInformation($"Getting documents page '{pageRequest}'...");
            _pageSortValidator.ValidateSortByProperty<Document>(pageRequest.SortBy);

            var items = await dbConnection.QueryAsync<Implementation.Models.Document>(pageRequest);
            var totalItemCount = await dbConnection.QueryScalarValueFunctionAsync<int>("[CoreFacade].[GetDocumentCount]");

            return new PageResponse<Document>(pageRequest)
            {
                Items = _mapper.Map<IList<Document>>(items),
                TotalItemCount = totalItemCount
            };
        }

        public async Task<Document> GetDocumentAsync(IDbConnection dbConnection, Guid documentId)
        {
            _logger.LogInformation($"Getting document '{documentId}'...");
            return _mapper.Map<Document>(await dbConnection.QuerySingleTableValueFunctionAsync<Implementation.Models.Document>("[CoreFacade].[GetDocument]", new { DocumentId = documentId }));
        }

        public async Task<Document> InsertDocumentAsync(IDbTransaction transaction, Document document)
        {
            _logger.LogInformation($"Inserting document '{document}'...");
            var documentToInsert = _mapper.Map<Implementation.Models.Document>(document);
            documentToInsert.DocumentId = Guid.NewGuid();
            documentToInsert.LastWrite = DateTimeOffset.Now;

            await transaction.Connection.InsertAsync(documentToInsert, transaction);
            var insertedDocument = await transaction.Connection.QuerySingleTableValueFunctionAsync<Implementation.Models.Document>("[CoreFacade].[GetDocument]", new { DocumentId = documentToInsert.DocumentId }, transaction);
            return _mapper.Map<Document>(insertedDocument);
        }
        
        public async Task<IEnumerable<Document>> GetPolicyDocumentsAsync(IDbConnection dbConnection, int policyId)
        {
            _logger.LogInformation($"Getting documents for policy '{policyId}'...");
            var items = await dbConnection.QueryTableValueFunctionAsync<Implementation.Models.Document>("[CoreFacade].[GetPolicyDocuments]", new { PolicyId = policyId });
            return _mapper.Map<IEnumerable<Document>>(items);
        }
        public Task<int> AddPolicyDocumentAsync(IDbTransaction dbTransaction, int policyId, Guid documentId)
        {
            _logger.LogInformation($"Adding document with Id '{documentId}' to policy with Id '{policyId}'...");
            return dbTransaction.Connection.ExecuteAsync("[CoreFacade].[AddPolicyDocument] @PolicyId, @DocumentId", new { PolicyId = policyId, DocumentId = documentId }, dbTransaction);
        }
        public Task<int> RemovePolicyDocumentAsync(IDbTransaction dbTransaction, int policyId, Guid documentId)
        {
            _logger.LogInformation($"Removing document with Id '{documentId}' from policy with Id '{policyId}'...");
            return dbTransaction.Connection.ExecuteAsync("[CoreFacade].[RemovePolicyDocument] @PolicyId, @DocumentId", new { PolicyId = policyId, DocumentId = documentId }, dbTransaction);
        }
        
        public async Task<IEnumerable<Document>> GetClaimDocumentsAsync(IDbConnection dbConnection, int claimId)
        {
            _logger.LogInformation($"Getting documents for claim '{claimId}'...");
            var items = await dbConnection.QueryTableValueFunctionAsync<Implementation.Models.Document>("[CoreFacade].[GetClaimDocuments]", new { ClaimId = claimId });
            return _mapper.Map<IEnumerable<Document>>(items);
        }
        public Task<int> AddClaimDocumentAsync(IDbTransaction dbTransaction, int claimId, Guid documentId)
        {
            _logger.LogInformation($"Adding document with Id '{documentId}' to claim with Id '{claimId}'...");
            return dbTransaction.Connection.ExecuteAsync("[CoreFacade].[AddClaimDocument] @ClaimId, @DocumentId", new { ClaimId = claimId, DocumentId = documentId }, dbTransaction);
        }
        public Task<int> RemoveClaimDocumentAsync(IDbTransaction dbTransaction, int claimId, Guid documentId)
        {
            _logger.LogInformation($"Removing document with Id '{documentId}' from claim with Id '{claimId}'...");
            return dbTransaction.Connection.ExecuteAsync("[CoreFacade].[RemoveClaimDocument] @ClaimId, @DocumentId", new { ClaimId = claimId, DocumentId = documentId }, dbTransaction);
        }
    }
}