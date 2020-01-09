

CREATE FUNCTION [CoreFacade].[GetPolicyDocuments]
(	
	@PolicyId int
)
RETURNS TABLE 
AS
RETURN 
(
	SELECT Documents.*
		FROM CoreModel.PolicyDocuments
		INNER JOIN CoreFacade.Documents
				ON PolicyDocuments.DocumentId = Documents.DocumentId
		WHERE PolicyId = @PolicyId
)