
CREATE VIEW [CoreFacade].[Claims]
WITH SCHEMABINDING
AS
	SELECT Claims.ClaimId
			,Claims.PolicyId
			,Claims.ObjectDocument
			,Claims.ObjectHash
		FROM CoreModel.Claims 
		WHERE ClaimId IN 
		(
			SELECT ClaimId 
				FROM SecurityModel.PrincipalClaims
				WHERE [PrincipalId] = DATABASE_PRINCIPAL_ID()
		)
GO


CREATE TRIGGER [CoreFacade].[TR_Claims_IOD]
   ON [CoreFacade].[Claims]
   INSTEAD OF DELETE
AS 
BEGIN
	SET NOCOUNT ON;

	DELETE FROM CoreModel.Claims
		WHERE ClaimId IN (SELECT ClaimId FROM deleted)
END
GO


CREATE TRIGGER [CoreFacade].[TR_Claims_IOU]
   ON [CoreFacade].[Claims]
   INSTEAD OF UPDATE
AS 
BEGIN
	SET NOCOUNT ON;

	UPDATE Claims
			SET ObjectDocument = inserted.ObjectDocument
			   ,ObjectHash = inserted.[ObjectHash]
		FROM CoreModel.Claims
		INNER JOIN inserted
				ON Claims.ClaimId = inserted.ClaimId
END
GO


CREATE TRIGGER [CoreFacade].[TR_Claims_IOI]
   ON [CoreFacade].[Claims]
   INSTEAD OF INSERT
AS 
BEGIN
	SET NOCOUNT ON;

	INSERT INTO CoreModel.Claims (PolicyId, [ObjectDocument], ObjectHash)
		SELECT inserted.PolicyId
				,inserted.[ObjectDocument]
				,inserted.[ObjectHash]
			FROM inserted
END