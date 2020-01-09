CREATE TABLE [CoreModel].[PolicyDocuments] (
    [PolicyDocumentId] INT              IDENTITY (1, 1) NOT NULL,
    [PolicyId]         INT              NOT NULL,
    [DocumentId]       UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_PolicyDocuments] PRIMARY KEY CLUSTERED ([PolicyDocumentId] ASC),
    CONSTRAINT [FK_PolicyDocuments_Documents] FOREIGN KEY ([DocumentId]) REFERENCES [CoreModel].[Documents] ([DocumentId]),
    CONSTRAINT [FK_PolicyDocuments_Policies] FOREIGN KEY ([PolicyId]) REFERENCES [CoreModel].[Policies] ([PolicyId]),
    CONSTRAINT [UQ_PolicyDocuments_PolicyId_DocumentId] UNIQUE NONCLUSTERED ([PolicyId] ASC, [DocumentId] ASC)
);

