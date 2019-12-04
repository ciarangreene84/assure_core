CREATE TABLE [SecurityModel].[PrincipalDocuments] (
    [PrincipalId] INT              NOT NULL,
    [DocumentId]  UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_PrincipalDocuments] PRIMARY KEY CLUSTERED ([PrincipalId] ASC, [DocumentId] ASC),
    CONSTRAINT [FK_PrincipalDocuments_Documents] FOREIGN KEY ([DocumentId]) REFERENCES [CoreModel].[Documents] ([DocumentId]) ON DELETE CASCADE,
    CONSTRAINT [FK_PrincipalDocuments_PrincipalDocuments] FOREIGN KEY ([PrincipalId], [DocumentId]) REFERENCES [SecurityModel].[PrincipalDocuments] ([PrincipalId], [DocumentId])
);

