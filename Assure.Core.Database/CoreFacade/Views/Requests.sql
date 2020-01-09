
CREATE VIEW [CoreFacade].[Requests]
WITH SCHEMABINDING
AS
	SELECT Requests.RequestId
			,Requests.[Type]
			,Requests.ObjectDocument
			,Requests.ObjectHash
		FROM CoreModel.Requests 
		WHERE RequestId IN 
		(
			SELECT RequestId 
				FROM SecurityModel.PrincipalRequests
				WHERE [PrincipalId] = DATABASE_PRINCIPAL_ID()
		)
GO


CREATE TRIGGER [CoreFacade].[TR_Requests_IOU]
   ON [CoreFacade].[Requests]
   INSTEAD OF UPDATE
AS 
BEGIN
	SET NOCOUNT ON;

	UPDATE Requests
			SET ObjectDocument = inserted.ObjectDocument
			   ,ObjectHash = inserted.[ObjectHash]
		FROM CoreModel.Requests
		INNER JOIN inserted
				ON Requests.RequestId = inserted.RequestId
END
GO


CREATE TRIGGER [CoreFacade].[TR_Requests_IOI]
   ON [CoreFacade].[Requests]
   INSTEAD OF INSERT
AS 
BEGIN
	SET NOCOUNT ON;

	INSERT INTO CoreModel.Requests ([Type], [ObjectDocument], ObjectHash)
		SELECT inserted.[Type]
				,inserted.[ObjectDocument]
				,inserted.[ObjectHash]
			FROM inserted
END
GO


CREATE TRIGGER [CoreFacade].[TR_Requests_IOD]
   ON [CoreFacade].[Requests]
   INSTEAD OF DELETE
AS 
BEGIN
	SET NOCOUNT ON;

	DELETE FROM CoreModel.Requests
		WHERE RequestId IN (SELECT RequestId FROM deleted)
END