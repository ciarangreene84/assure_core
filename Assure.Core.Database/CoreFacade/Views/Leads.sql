CREATE VIEW [CoreFacade].[Leads]
WITH SCHEMABINDING
AS
	SELECT Leads.LeadId
			,Leads.[Name]
			,Leads.ObjectDocument
			,Leads.ObjectHash
		FROM CoreModel.Leads 
		WHERE LeadId IN 
		(
			SELECT LeadId 
				FROM SecurityModel.PrincipalLeads
				WHERE [PrincipalId] = DATABASE_PRINCIPAL_ID()
		)
GO

CREATE TRIGGER [CoreFacade].[TR_Leads_IOU]
   ON [CoreFacade].[Leads]
   INSTEAD OF UPDATE
AS 
BEGIN
	SET NOCOUNT ON;

	UPDATE Leads
			SET [Name] = inserted.[Name]
			   ,ObjectDocument = inserted.ObjectDocument
			   ,ObjectHash = inserted.[ObjectHash]
		FROM CoreModel.Leads
		INNER JOIN inserted
				ON Leads.LeadId = inserted.LeadId
END
GO

CREATE TRIGGER [CoreFacade].[TR_Leads_IOI]
   ON [CoreFacade].[Leads]
   INSTEAD OF INSERT
AS 
BEGIN
	SET NOCOUNT ON;

	INSERT INTO CoreModel.Leads ([Name], [ObjectDocument], ObjectHash)
		SELECT inserted.[Name]
				,inserted.[ObjectDocument]
				,inserted.[ObjectHash]
			FROM inserted
END
GO

CREATE TRIGGER [CoreFacade].[TR_Leads_IOD]
   ON [CoreFacade].[Leads]
   INSTEAD OF DELETE
AS 
BEGIN
	SET NOCOUNT ON;

	DELETE FROM CoreModel.Leads
		WHERE LeadId IN (SELECT LeadId FROM deleted)
END