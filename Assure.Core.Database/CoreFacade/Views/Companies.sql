CREATE VIEW [CoreFacade].[Companies]
WITH SCHEMABINDING
AS
	SELECT Companies.CompanyId
			,Companies.[Name]
			,Companies.ObjectDocument
			,Companies.ObjectHash
		FROM CoreModel.Companies 
		WHERE CompanyId IN 
		(
			SELECT CompanyId 
				FROM SecurityModel.PrincipalCompanies
				WHERE [PrincipalId] = DATABASE_PRINCIPAL_ID()
		)
GO

CREATE TRIGGER [CoreFacade].[TR_Companies_IOU]
   ON [CoreFacade].[Companies]
   INSTEAD OF UPDATE
AS 
BEGIN
	SET NOCOUNT ON;

	UPDATE Companies
			SET [Name] = inserted.[Name]
			   ,ObjectDocument = inserted.ObjectDocument
			   ,ObjectHash = inserted.[ObjectHash]
		FROM CoreModel.Companies
		INNER JOIN inserted
				ON Companies.CompanyId = inserted.CompanyId
END
GO

CREATE TRIGGER [CoreFacade].[TR_Companies_IOI]
   ON [CoreFacade].[Companies]
   INSTEAD OF INSERT
AS 
BEGIN
	SET NOCOUNT ON;

	INSERT INTO CoreModel.Companies ([Name], [ObjectDocument], ObjectHash)
		SELECT inserted.[Name]
				,inserted.[ObjectDocument]
				,inserted.[ObjectHash]
			FROM inserted
END
GO

CREATE TRIGGER [CoreFacade].[TR_Companies_IOD]
   ON [CoreFacade].[Companies]
   INSTEAD OF DELETE
AS 
BEGIN
	SET NOCOUNT ON;

	DELETE FROM CoreModel.Companies
		WHERE CompanyId IN (SELECT CompanyId FROM deleted)
END