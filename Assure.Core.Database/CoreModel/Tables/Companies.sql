CREATE TABLE [CoreModel].[Companies] (
    [CompanyId]      INT           IDENTITY (1, 1) NOT NULL,
    [Name]           VARCHAR (128) NOT NULL,
    [ObjectDocument] VARCHAR (MAX) NOT NULL,
    [ObjectHash]     INT           NOT NULL,
    CONSTRAINT [PK_Companies] PRIMARY KEY CLUSTERED ([CompanyId] ASC),
    CONSTRAINT [CK_Companies_Name] CHECK ((0)<len([Name])),
    CONSTRAINT [CK_Companies_ObjectDocument] CHECK (isjson([ObjectDocument])>(0)),
    CONSTRAINT [IX_Companies_CompanyId] UNIQUE NONCLUSTERED ([CompanyId] ASC),
    CONSTRAINT [UQ_Companies_Name] UNIQUE NONCLUSTERED ([Name] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_Companies_ObjectHash]
    ON [CoreModel].[Companies]([ObjectHash] ASC);


GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [CoreModel].[TR_Companies_AI]
   ON [CoreModel].[Companies]
   AFTER INSERT
AS 
BEGIN
	SET NOCOUNT ON;

	INSERT INTO SecurityModel.PrincipalCompanies (PrincipalId, CompanyId)
		SELECT DATABASE_PRINCIPAL_ID()
				,CompanyId
			FROM inserted

END