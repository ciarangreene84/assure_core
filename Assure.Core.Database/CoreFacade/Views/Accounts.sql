CREATE VIEW [CoreFacade].[Accounts]
WITH SCHEMABINDING
AS
	SELECT Accounts.AccountId
			,AccountTypes.[Name] AS [Type]
			,Accounts.[Name]
			,Accounts.ObjectDocument
			,Accounts.ObjectHash
		FROM CoreModel.Accounts 
		INNER JOIN StaticModel.AccountTypes 
				ON Accounts.AccountTypeId = AccountTypes.AccountTypeId
		WHERE AccountId IN 
		(
			SELECT AccountId 
				FROM SecurityModel.PrincipalAccounts
				WHERE [PrincipalId] = DATABASE_PRINCIPAL_ID()
		)
GO

CREATE TRIGGER [CoreFacade].[TR_Accounts_IOD]
   ON [CoreFacade].[Accounts]
   INSTEAD OF DELETE
AS 
BEGIN
	SET NOCOUNT ON;

	DELETE FROM CoreModel.Accounts
		WHERE AccountId IN (SELECT AccountId FROM deleted)
END
GO

CREATE TRIGGER [CoreFacade].[TR_Accounts_IOI]
   ON [CoreFacade].[Accounts]
   INSTEAD OF INSERT
AS 
BEGIN
	SET NOCOUNT ON;

	INSERT INTO CoreModel.Accounts (AccountTypeId, [Name], [ObjectDocument], ObjectHash)
		SELECT accountTypes.AccountTypeId
				,inserted.[Name]
				,inserted.[ObjectDocument]
				,inserted.[ObjectHash]
			FROM inserted
			INNER JOIN StaticModel.AccountTypes
					ON inserted.[Type] = accountTypes.[Name]
END
GO

CREATE TRIGGER [CoreFacade].[TR_Accounts_IOU]
   ON [CoreFacade].[Accounts]
   INSTEAD OF UPDATE
AS 
BEGIN
	SET NOCOUNT ON;

	UPDATE accounts
			SET [Name] = inserted.[Name]
			   ,ObjectDocument = inserted.ObjectDocument
			   ,ObjectHash = inserted.[ObjectHash]
		FROM CoreModel.Accounts
		INNER JOIN inserted
				ON accounts.AccountId = inserted.AccountId
END
GO

