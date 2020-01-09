

CREATE VIEW [CoreFacade].[Documents]
WITH SCHEMABINDING
AS
	SELECT Documents.DocumentId
			,Documents.[Name]
			,Documents.[Type]
			,Documents.[LastWrite]
			,CONVERT(varbinary(MAX), NULL) AS Data
		FROM CoreModel.Documents 
		WHERE DocumentId IN 
		(
			SELECT DocumentId 
				FROM SecurityModel.PrincipalDocuments
				WHERE [PrincipalId] = DATABASE_PRINCIPAL_ID()
		)
GO


CREATE TRIGGER [CoreFacade].[TR_Documents_IOI]
   ON [CoreFacade].[Documents]
   INSTEAD OF INSERT
AS 
BEGIN
	SET NOCOUNT ON;

	INSERT INTO CoreModel.Documents (DocumentId, [Name], [Type], [LastWrite], [Data])
		SELECT inserted.DocumentId
				,inserted.[Name]
				,inserted.[Type]
				,inserted.[LastWrite]
				,inserted.[Data]
			FROM inserted
END