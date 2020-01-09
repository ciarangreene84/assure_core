

CREATE VIEW [CoreFacade].[Invoices]
WITH SCHEMABINDING
AS
	SELECT Invoices.InvoiceId
			,Invoices.Identifier
			,Products.[Name] AS [Product]
			,CurrencyAlpha3
			,Invoices.Amount
			,Invoices.DateTime
			,Invoices.ObjectDocument
			,Invoices.ObjectHash
		FROM CoreModel.Invoices 
		INNER JOIN StaticModel.Products
				ON Invoices.ProductId = Products.ProductId
		WHERE [Identity].[IsUserInRole]('Invoice Administrator') = 'true'
		   OR InvoiceId IN 
			(
				SELECT InvoiceId 
					FROM SecurityModel.PrincipalInvoices
					WHERE [PrincipalId] = DATABASE_PRINCIPAL_ID()
			)
GO

CREATE TRIGGER [CoreFacade].[TR_Invoices_IOD]
   ON [CoreFacade].[Invoices]
   INSTEAD OF DELETE
AS 
BEGIN
	SET NOCOUNT ON;

	DELETE FROM CoreModel.Invoices
		WHERE InvoiceId IN (SELECT InvoiceId FROM deleted)
END
GO

CREATE TRIGGER [CoreFacade].[TR_Invoices_IOI]
   ON [CoreFacade].[Invoices]
   INSTEAD OF INSERT
AS 
BEGIN
	SET NOCOUNT ON;

	INSERT INTO CoreModel.Invoices 
			(
				 [Identifier]
				,[ProductId]
				,[CurrencyAlpha3]
				,[Amount]
				,[DateTime]
				,[ObjectDocument]
				,[ObjectHash]
			)
		SELECT inserted.[Identifier]
				,Products.[ProductId]
				,inserted.[CurrencyAlpha3]
				,inserted.[Amount]
				,inserted.[DateTime]
				,inserted.[ObjectDocument]
				,inserted.[ObjectHash]
			FROM inserted
			INNER JOIN StaticModel.Products
					ON inserted.Product = Products.[Name]
END
GO

CREATE TRIGGER [CoreFacade].[TR_Invoices_IOU]
   ON [CoreFacade].[Invoices]
   INSTEAD OF UPDATE
AS 
BEGIN
	SET NOCOUNT ON;

	UPDATE invoices
			SET ObjectDocument = inserted.ObjectDocument
			   ,ObjectHash = inserted.[ObjectHash]
		FROM CoreModel.Invoices
		INNER JOIN inserted
				ON invoices.InvoiceId = inserted.InvoiceId
END
GO

