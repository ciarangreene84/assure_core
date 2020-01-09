CREATE TABLE [Identity].[UserClaims] (
    [UserClaimId] INT              IDENTITY (1, 1) NOT NULL,
    [UserId]      UNIQUEIDENTIFIER NOT NULL,
    [ClaimType]   NVARCHAR (MAX)   NULL,
    [ClaimValue]  NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED ([UserClaimId] ASC),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [Identity].[Users] ([UserId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId]
    ON [Identity].[UserClaims]([UserId] ASC);

