
CREATE FUNCTION [CoreFacade].[GetDocumentCount]
(

)
RETURNS int
AS
BEGIN

	DECLARE @ItemCount int
	IF ([Identity].[IsUserInRole]('Document Administrator') = 'true')
	BEGIN
		SET @ItemCount = (SELECT COUNT(DocumentId) FROM CoreModel.Documents) 
	END
	ELSE
	BEGIN
		SET @ItemCount = (SELECT COUNT(DocumentId) FROM SecurityModel.PrincipalDocuments WHERE [PrincipalId] = DATABASE_PRINCIPAL_ID())
	END

	-- Return the result of the function
	RETURN @ItemCount;

END
GO
GRANT EXECUTE
    ON OBJECT::[CoreFacade].[GetDocumentCount] TO [AssureCustomer]
    AS [dbo];

