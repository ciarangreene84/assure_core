CREATE VIEW [CoreFacade].[Customers]
WITH SCHEMABINDING
AS
	SELECT Customers.CustomerId
			,Customers.[Name]
			,Customers.ObjectDocument
			,Customers.ObjectHash
		FROM CoreModel.Customers 
		WHERE CustomerId IN 
		(
			SELECT CustomerId 
				FROM SecurityModel.PrincipalCustomers
				WHERE [PrincipalId] = DATABASE_PRINCIPAL_ID()
		)
GO

CREATE TRIGGER [CoreFacade].[TR_Customers_IOD]
   ON [CoreFacade].[Customers]
   INSTEAD OF DELETE
AS 
BEGIN
	SET NOCOUNT ON;

	DELETE FROM CoreModel.Customers
		WHERE CustomerId IN (SELECT CustomerId FROM deleted)
END
GO

CREATE TRIGGER [CoreFacade].[TR_Customers_IOI]
   ON [CoreFacade].[Customers]
   INSTEAD OF INSERT
AS 
BEGIN
	SET NOCOUNT ON;

	INSERT INTO CoreModel.Customers ([Name], [ObjectDocument], ObjectHash)
		SELECT inserted.[Name]
				,inserted.[ObjectDocument]
				,inserted.[ObjectHash]
			FROM inserted
END
GO

CREATE TRIGGER [CoreFacade].[TR_Customers_IOU]
   ON [CoreFacade].[Customers]
   INSTEAD OF UPDATE
AS 
BEGIN
	SET NOCOUNT ON;

	UPDATE customers
			SET [Name] = inserted.[Name]
			   ,ObjectDocument = inserted.ObjectDocument
			   ,ObjectHash = inserted.[ObjectHash]
		FROM CoreModel.Customers
		INNER JOIN inserted
				ON customers.CustomerId = inserted.CustomerId
END
GO

