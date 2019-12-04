CREATE TABLE [Identity].[RoleClaims] (
    [RoleClaimId] INT              IDENTITY (1, 1) NOT NULL,
    [RoleId]      UNIQUEIDENTIFIER NOT NULL,
    [ClaimType]   NVARCHAR (MAX)   NULL,
    [ClaimValue]  NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED ([RoleClaimId] ASC),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Identity].[Roles] ([RoleId]) ON DELETE CASCADE
);




GO
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId]
    ON [Identity].[RoleClaims]([RoleId] ASC);

