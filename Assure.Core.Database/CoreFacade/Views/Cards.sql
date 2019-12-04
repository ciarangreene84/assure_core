CREATE VIEW [CoreFacade].[Cards]
WITH SCHEMABINDING
AS
	SELECT Cards.CardId
			,Cards.Number
			,Cards.ObjectDocument
			,Cards.ObjectHash
		FROM CoreModel.Cards 
		WHERE CardId IN 
		(
			SELECT CardId 
				FROM SecurityModel.PrincipalCards
				WHERE [PrincipalId] = DATABASE_PRINCIPAL_ID()
		)
GO

CREATE TRIGGER [CoreFacade].[TR_Cards_IOD]
   ON [CoreFacade].[Cards]
   INSTEAD OF DELETE
AS 
BEGIN
	SET NOCOUNT ON;

	DELETE FROM CoreModel.Cards
		WHERE CardId IN (SELECT CardId FROM deleted)
END
GO

CREATE TRIGGER [CoreFacade].[TR_Cards_IOI]
   ON [CoreFacade].[Cards]
   INSTEAD OF INSERT
AS 
BEGIN
	SET NOCOUNT ON;

	INSERT INTO CoreModel.Cards (Number, [ObjectDocument], ObjectHash)
		SELECT inserted.Number
				,inserted.[ObjectDocument]
				,inserted.[ObjectHash]
			FROM inserted
END
GO

CREATE TRIGGER [CoreFacade].[TR_Cards_IOU]
   ON [CoreFacade].[Cards]
   INSTEAD OF UPDATE
AS 
BEGIN
	SET NOCOUNT ON;

	UPDATE cards
			SET Number = inserted.Number
			   ,ObjectDocument = inserted.ObjectDocument
			   ,ObjectHash = inserted.[ObjectHash]
		FROM CoreModel.Cards
		INNER JOIN inserted
				ON cards.CardId = inserted.CardId
END
GO

