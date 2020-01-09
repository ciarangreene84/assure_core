CREATE TABLE [Identity].[Roles] (
    [RoleId]           UNIQUEIDENTIFIER NOT NULL,
    [Name]             NVARCHAR (256)   NOT NULL,
    [NormalizedName]   NVARCHAR (256)   NOT NULL,
    [ConcurrencyStamp] NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED ([RoleId] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex]
    ON [Identity].[Roles]([NormalizedName] ASC);

