CREATE TABLE [SecurityModel].[PrincipalQuotes] (
    [PrincipalId] INT NOT NULL,
    [QuoteId]     INT NOT NULL,
    CONSTRAINT [PK_PrincipalQuotes] PRIMARY KEY CLUSTERED ([PrincipalId] ASC, [QuoteId] ASC),
    CONSTRAINT [FK_PrincipalQuotes_PrincipalQuotes] FOREIGN KEY ([PrincipalId], [QuoteId]) REFERENCES [SecurityModel].[PrincipalQuotes] ([PrincipalId], [QuoteId]),
    CONSTRAINT [FK_PrincipalQuotes_Quotes] FOREIGN KEY ([QuoteId]) REFERENCES [CoreModel].[Quotes] ([QuoteId]) ON DELETE CASCADE
);

