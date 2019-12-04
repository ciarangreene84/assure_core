CREATE TABLE [SecurityModel].[PrincipalCompanies] (
    [PrincipalId] INT NOT NULL,
    [CompanyId]   INT NOT NULL,
    CONSTRAINT [PK_PrincipalCompanies] PRIMARY KEY CLUSTERED ([PrincipalId] ASC, [CompanyId] ASC),
    CONSTRAINT [FK_PrincipalCompanies_Companies] FOREIGN KEY ([CompanyId]) REFERENCES [CoreModel].[Companies] ([CompanyId]) ON DELETE CASCADE,
    CONSTRAINT [FK_PrincipalCompanies_PrincipalCompanies] FOREIGN KEY ([PrincipalId], [CompanyId]) REFERENCES [SecurityModel].[PrincipalCompanies] ([PrincipalId], [CompanyId])
);

