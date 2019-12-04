CREATE TABLE [SecurityModel].[PrincipalClaims] (
    [PrincipalId] INT NOT NULL,
    [ClaimId]     INT NOT NULL,
    CONSTRAINT [PK_PrincipalClaims] PRIMARY KEY CLUSTERED ([PrincipalId] ASC, [ClaimId] ASC),
    CONSTRAINT [FK_PrincipalClaims_Claims] FOREIGN KEY ([ClaimId]) REFERENCES [CoreModel].[Claims] ([ClaimId]) ON DELETE CASCADE,
    CONSTRAINT [FK_PrincipalClaims_PrincipalClaims] FOREIGN KEY ([PrincipalId], [ClaimId]) REFERENCES [SecurityModel].[PrincipalClaims] ([PrincipalId], [ClaimId])
);

