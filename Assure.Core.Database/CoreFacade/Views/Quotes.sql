
CREATE VIEW [CoreFacade].[Quotes]
WITH SCHEMABINDING
AS
	SELECT Quotes.QuoteId
			,products.Name AS [Product]
			,Quotes.StartDateTime
			,Quotes.EndDateTime
			,Quotes.[ObjectDocument]
			,quotes.ObjectHash
		FROM CoreModel.Quotes 
		INNER JOIN StaticModel.Products
				ON quotes.ProductId = Products.ProductId
		--WHERE QuoteId IN 
		--(
		--	SELECT QuoteId 
		--		FROM SecurityModel.PrincipalQuotes
		--		WHERE [PrincipalId] = DATABASE_PRINCIPAL_ID()
		--)
GO




CREATE TRIGGER [CoreFacade].[TR_Quotes_IOU]
   ON [CoreFacade].[Quotes]
   INSTEAD OF UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE Quotes
			SET ObjectDocument = inserted.ObjectDocument
			   ,ObjectHash = inserted.[ObjectHash]
		FROM CoreModel.Quotes
		INNER JOIN inserted
				ON quotes.QuoteId = inserted.QuoteId
END
GO




CREATE TRIGGER [CoreFacade].[TR_Quotes_IOI]
   ON [CoreFacade].[Quotes]
   INSTEAD OF INSERT
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO CoreModel.Quotes (ProductId, StartDateTime, EndDateTime, [ObjectDocument], ObjectHash)
		SELECT products.ProductId
				,inserted.StartDateTime
				,inserted.EndDateTime
				,inserted.[ObjectDocument]
				,inserted.[ObjectHash]
			FROM inserted
			INNER JOIN StaticModel.Products
					ON inserted.[Product] = Products.[Name]
END
GO





CREATE TRIGGER [CoreFacade].[TR_Quotes_IOD]
   ON [CoreFacade].[Quotes]
   INSTEAD OF DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DELETE FROM CoreModel.Quotes
		WHERE QuoteId IN (SELECT QuoteId FROM deleted)
END