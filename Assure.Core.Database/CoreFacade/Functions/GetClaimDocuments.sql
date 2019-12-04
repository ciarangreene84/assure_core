

CREATE FUNCTION [CoreFacade].[GetClaimDocuments]
(	
	@ClaimId int
)
RETURNS TABLE 
AS
RETURN 
(
	SELECT Documents.*
		FROM CoreModel.ClaimDocuments
		INNER JOIN CoreFacade.Documents
				ON ClaimDocuments.DocumentId = Documents.DocumentId
		WHERE ClaimId = @ClaimId
)