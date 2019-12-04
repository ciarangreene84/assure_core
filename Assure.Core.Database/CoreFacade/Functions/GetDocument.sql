CREATE FUNCTION [CoreFacade].[GetDocument]
(	
	@DocumentId uniqueidentifier
)
RETURNS TABLE 
AS
RETURN 
(
	SELECT Documents.DocumentId
			,Documents.[Name]
			,Documents.[Type]
			,Documents.[LastWrite]
			,Documents.[Data]
		FROM CoreModel.Documents 
		WHERE DocumentId = @DocumentId
		  AND DocumentId IN 
			(
				SELECT DocumentId 
					FROM SecurityModel.PrincipalDocuments
					WHERE [PrincipalId] = DATABASE_PRINCIPAL_ID()
			)
)