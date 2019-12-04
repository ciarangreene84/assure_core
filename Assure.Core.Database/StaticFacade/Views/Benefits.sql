CREATE VIEW [StaticFacade].[Benefits]
WITH SCHEMABINDING
AS
	SELECT BenefitId
			,[Name]
			,[ObjectDocument]
			,[ObjectHash]
		FROM StaticModel.Benefits
GO

CREATE TRIGGER [StaticFacade].[TR_Benefits_IOD]
   ON [StaticFacade].[Benefits]
   INSTEAD OF DELETE
AS 
BEGIN
	SET NOCOUNT ON;

	DELETE FROM StaticModel.Benefits
		WHERE BenefitId IN (SELECT BenefitId FROM deleted)
END
GO

CREATE TRIGGER [StaticFacade].[TR_Benefits_IOI]
   ON [StaticFacade].[Benefits]
   INSTEAD OF INSERT
AS 
BEGIN
	SET NOCOUNT ON;

	INSERT INTO StaticModel.Benefits ([BenefitId], [Name], [ObjectDocument], [ObjectHash])
		SELECT inserted.[BenefitId]
				,inserted.[Name]
				,inserted.[ObjectDocument]
				,inserted.[ObjectHash]
			FROM inserted
END
GO

CREATE TRIGGER [StaticFacade].[TR_Benefits_IOU]
   ON [StaticFacade].[Benefits]
   INSTEAD OF UPDATE
AS 
BEGIN
	SET NOCOUNT ON;

	UPDATE Benefits
			SET [Name] = inserted.[Name]
			   ,ObjectDocument = inserted.ObjectDocument
			   ,ObjectHash = inserted.[ObjectHash]
		FROM StaticModel.Benefits
		INNER JOIN inserted
				ON Benefits.BenefitId = inserted.BenefitId
END
GO

