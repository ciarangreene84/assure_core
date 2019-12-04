CREATE TABLE [CoreModel].[Accounts] (
    [AccountId]      INT           IDENTITY (1, 1) NOT NULL,
    [AccountTypeId]  INT           NOT NULL,
    [Name]           VARCHAR (128) NOT NULL,
    [ObjectDocument] VARCHAR (MAX) NOT NULL,
    [ObjectHash]     INT           NOT NULL,
    CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED ([AccountId] ASC),
    CONSTRAINT [CK_Accounts_Name] CHECK ((0)<len([Name])),
    CONSTRAINT [CK_Accounts_ObjectDocument] CHECK (isjson([ObjectDocument])>(0)),
    CONSTRAINT [FK_Accounts_AccountTypes] FOREIGN KEY ([AccountTypeId]) REFERENCES [StaticModel].[AccountTypes] ([AccountTypeId]),
    CONSTRAINT [IX_Accounts_AccountId] UNIQUE NONCLUSTERED ([AccountId] ASC),
    CONSTRAINT [UQ_Accounts_Name] UNIQUE NONCLUSTERED ([Name] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_Accounts_ObjectHash]
    ON [CoreModel].[Accounts]([ObjectHash] ASC);


GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [CoreModel].[TR_Accounts_AI]
   ON [CoreModel].[Accounts]
   AFTER INSERT
AS 
BEGIN
	SET NOCOUNT ON;

	INSERT INTO SecurityModel.PrincipalAccounts (PrincipalId, AccountId)
		SELECT DATABASE_PRINCIPAL_ID()
				,AccountId
			FROM inserted

END