CREATE TABLE [Identity].[Users] (
    [UserId]               UNIQUEIDENTIFIER   NOT NULL,
    [UserName]             NVARCHAR (256)     NOT NULL,
    [NormalizedUserName]   NVARCHAR (256)     NOT NULL,
    [Email]                NVARCHAR (256)     NOT NULL,
    [NormalizedEmail]      NVARCHAR (256)     NOT NULL,
    [EmailConfirmed]       BIT                NOT NULL,
    [PasswordHash]         NVARCHAR (MAX)     NULL,
    [SecurityStamp]        NVARCHAR (MAX)     NULL,
    [ConcurrencyStamp]     NVARCHAR (MAX)     NULL,
    [PhoneNumber]          NVARCHAR (MAX)     NULL,
    [PhoneNumberConfirmed] BIT                NOT NULL,
    [TwoFactorEnabled]     BIT                NOT NULL,
    [LockoutEnd]           DATETIMEOFFSET (7) NULL,
    [LockoutEnabled]       BIT                NOT NULL,
    [AccessFailedCount]    INT                NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED ([UserId] ASC)
);


GO
CREATE NONCLUSTERED INDEX [EmailIndex]
    ON [Identity].[Users]([NormalizedEmail] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex]
    ON [Identity].[Users]([NormalizedUserName] ASC) WHERE ([NormalizedUserName] IS NOT NULL);

