CREATE VIEW [CoreFacade].[Policies]
WITH SCHEMABINDING
AS
	SELECT Policies.PolicyId
			,Products.[Name] AS [Product]
			,Policies.ObjectDocument
			,Policies.ObjectHash
			,Policies.StartDateTime
			,Policies.EndDateTime
		FROM CoreModel.Policies 
		INNER JOIN StaticModel.Products
				ON Policies.ProductId = Products.ProductId
		WHERE PolicyId IN 
		(
			SELECT PolicyId 
				FROM SecurityModel.PrincipalPolicies
				WHERE [PrincipalId] = DATABASE_PRINCIPAL_ID()
		)
GO

CREATE TRIGGER [CoreFacade].[TR_Policies_IOD]
   ON [CoreFacade].[Policies]
   INSTEAD OF DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DELETE FROM CoreModel.Policies
		WHERE PolicyId IN (SELECT PolicyId FROM deleted)
END
GO

CREATE TRIGGER [CoreFacade].[TR_Policies_IOI]
   ON [CoreFacade].[Policies]
   INSTEAD OF INSERT
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO CoreModel.Policies (ProductId, StartDateTime, EndDateTime, [ObjectDocument], ObjectHash)
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

CREATE TRIGGER [CoreFacade].[TR_Policies_IOU]
   ON [CoreFacade].[Policies]
   INSTEAD OF UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE Policies
			SET ObjectDocument = inserted.ObjectDocument
			   ,ObjectHash = inserted.[ObjectHash]
		FROM CoreModel.Policies
		INNER JOIN inserted
				ON policies.PolicyId = inserted.PolicyId
END
GO

