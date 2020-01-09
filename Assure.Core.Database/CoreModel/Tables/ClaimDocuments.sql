CREATE TABLE [CoreModel].[ClaimDocuments] (
    [ClaimDocumentId] INT              IDENTITY (1, 1) NOT NULL,
    [ClaimId]         INT              NOT NULL,
    [DocumentId]      UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_ClaimDocuments] PRIMARY KEY CLUSTERED ([ClaimDocumentId] ASC),
    CONSTRAINT [FK_ClaimDocuments_Claims] FOREIGN KEY ([ClaimId]) REFERENCES [CoreModel].[Claims] ([ClaimId]),
    CONSTRAINT [FK_ClaimDocuments_Documents] FOREIGN KEY ([DocumentId]) REFERENCES [CoreModel].[Documents] ([DocumentId]),
    CONSTRAINT [UQ_ClaimDocuments_ClaimId_DocumentId] UNIQUE NONCLUSTERED ([ClaimId] ASC, [DocumentId] ASC)
);

