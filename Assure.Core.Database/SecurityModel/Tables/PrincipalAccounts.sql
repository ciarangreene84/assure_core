CREATE TABLE [SecurityModel].[PrincipalAccounts] (
    [PrincipalId] INT NOT NULL,
    [AccountId]   INT NOT NULL,
    CONSTRAINT [PK_PrincipalAccounts] PRIMARY KEY CLUSTERED ([PrincipalId] ASC, [AccountId] ASC),
    CONSTRAINT [FK_PrincipalAccounts_Accounts] FOREIGN KEY ([AccountId]) REFERENCES [CoreModel].[Accounts] ([AccountId]) ON DELETE CASCADE,
    CONSTRAINT [FK_PrincipalAccounts_PrincipalAccounts] FOREIGN KEY ([PrincipalId], [AccountId]) REFERENCES [SecurityModel].[PrincipalAccounts] ([PrincipalId], [AccountId])
);

