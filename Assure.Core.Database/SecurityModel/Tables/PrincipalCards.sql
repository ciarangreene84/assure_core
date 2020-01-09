CREATE TABLE [SecurityModel].[PrincipalCards] (
    [PrincipalId] INT NOT NULL,
    [CardId]      INT NOT NULL,
    CONSTRAINT [PK_PrincipalCards] PRIMARY KEY CLUSTERED ([PrincipalId] ASC, [CardId] ASC),
    CONSTRAINT [FK_PrincipalCards_Cards] FOREIGN KEY ([CardId]) REFERENCES [CoreModel].[Cards] ([CardId]) ON DELETE CASCADE,
    CONSTRAINT [FK_PrincipalCards_PrincipalCards] FOREIGN KEY ([PrincipalId], [CardId]) REFERENCES [SecurityModel].[PrincipalCards] ([PrincipalId], [CardId])
);

