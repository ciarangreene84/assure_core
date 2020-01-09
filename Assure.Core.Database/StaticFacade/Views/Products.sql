CREATE VIEW [StaticFacade].[Products]
WITH SCHEMABINDING
AS
	SELECT ProductId
			,[Name]
			,[ObjectDocument]
			,[ObjectHash]
		FROM StaticModel.Products
GO

CREATE TRIGGER [StaticFacade].[TR_Products_IOD]
   ON [StaticFacade].[Products]
   INSTEAD OF DELETE
AS 
BEGIN
	SET NOCOUNT ON;

	DELETE FROM StaticModel.Products
		WHERE ProductId IN (SELECT ProductId FROM deleted)
END
GO

CREATE TRIGGER [StaticFacade].[TR_Products_IOI]
   ON [StaticFacade].[Products]
   INSTEAD OF INSERT
AS 
BEGIN
	SET NOCOUNT ON;

	INSERT INTO StaticModel.Products ([ProductId], [Name], [ObjectDocument], [ObjectHash])
		SELECT inserted.[ProductId]
				,inserted.[Name]
				,inserted.[ObjectDocument]
				,inserted.[ObjectHash]
			FROM inserted
END
GO

CREATE TRIGGER [StaticFacade].[TR_Products_IOU]
   ON [StaticFacade].[Products]
   INSTEAD OF UPDATE
AS 
BEGIN
	SET NOCOUNT ON;

	UPDATE Products
			SET [Name] = inserted.[Name]
			   ,ObjectDocument = inserted.ObjectDocument
			   ,ObjectHash = inserted.[ObjectHash]
		FROM StaticModel.Products
		INNER JOIN inserted
				ON Products.ProductId = inserted.ProductId
END
GO

