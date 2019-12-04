CREATE TABLE [SecurityModel].[PrincipalPolicies] (
    [PrincipalId] INT NOT NULL,
    [PolicyId]    INT NOT NULL,
    CONSTRAINT [PK_PrincipalPolicies] PRIMARY KEY CLUSTERED ([PrincipalId] ASC, [PolicyId] ASC),
    CONSTRAINT [FK_PrincipalPolicies_Policies] FOREIGN KEY ([PolicyId]) REFERENCES [CoreModel].[Policies] ([PolicyId]) ON DELETE CASCADE,
    CONSTRAINT [FK_PrincipalPolicies_PrincipalPolicies] FOREIGN KEY ([PrincipalId], [PolicyId]) REFERENCES [SecurityModel].[PrincipalPolicies] ([PrincipalId], [PolicyId])
);

