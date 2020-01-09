CREATE TABLE [CoreModel].[Customers] (
    [CustomerId]     INT           IDENTITY (1, 1) NOT NULL,
    [Name]           VARCHAR (128) NOT NULL,
    [ObjectDocument] VARCHAR (MAX) NOT NULL,
    [ObjectHash]     INT           NOT NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED ([CustomerId] ASC),
    CONSTRAINT [CK_Customers_Name] CHECK ((0)<len([Name])),
    CONSTRAINT [CK_Customers_ObjectDocument] CHECK (isjson([ObjectDocument])>(0)),
    CONSTRAINT [IX_Customers_CustomerId] UNIQUE NONCLUSTERED ([CustomerId] ASC)
	--,CONSTRAINT [UQ_Customers_Name] UNIQUE NONCLUSTERED ([Name] ASC)
);




GO
CREATE NONCLUSTERED INDEX [IX_Customers_ObjectHash]
    ON [CoreModel].[Customers]([ObjectHash] ASC);


GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [CoreModel].[TR_Customers_AI]
   ON [CoreModel].[Customers]
   AFTER INSERT
AS 
BEGIN
	SET NOCOUNT ON;

	INSERT INTO SecurityModel.PrincipalCustomers (PrincipalId, CustomerId)
		SELECT DATABASE_PRINCIPAL_ID()
				,CustomerId
			FROM inserted

END