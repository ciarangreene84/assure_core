CREATE TABLE [CoreModel].[Invoices] (
    [InvoiceId]      INT                IDENTITY (1, 1) NOT NULL,
    [Identifier]     VARCHAR (64)       NOT NULL,
    [ProductId]      INT                NOT NULL,
    [CurrencyAlpha3] CHAR (3)           NOT NULL,
    [Amount]         DECIMAL (28, 8)    NOT NULL,
    [DateTime]       DATETIMEOFFSET (7) NOT NULL,
    [ObjectDocument] VARCHAR (MAX)      NOT NULL,
    [ObjectHash]     INT                NOT NULL,
    CONSTRAINT [PK_Invoices] PRIMARY KEY CLUSTERED ([InvoiceId] ASC),
    CONSTRAINT [CK_Invoices_ObjectDocument] CHECK (isjson([ObjectDocument])>(0)),
    CONSTRAINT [FK_Invoices_Currencies] FOREIGN KEY ([CurrencyAlpha3]) REFERENCES [StaticModel].[Currencies] ([Alpha3]),
    CONSTRAINT [FK_Invoices_Products] FOREIGN KEY ([ProductId]) REFERENCES [StaticModel].[Products] ([ProductId]),
    CONSTRAINT [IX_Invoices_InvoiceId] UNIQUE NONCLUSTERED ([InvoiceId] ASC),
    CONSTRAINT [UQ_Invoices_Identifier] UNIQUE NONCLUSTERED ([Identifier] ASC)
);






GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [CoreModel].[TR_Invoices_AI]
   ON [CoreModel].[Invoices]
   AFTER INSERT
AS 
BEGIN
	SET NOCOUNT ON;

	INSERT INTO SecurityModel.PrincipalInvoices (PrincipalId, InvoiceId)
		SELECT DATABASE_PRINCIPAL_ID()
				,InvoiceId
			FROM inserted

END