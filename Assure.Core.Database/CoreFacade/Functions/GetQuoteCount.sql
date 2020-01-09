

CREATE FUNCTION [CoreFacade].[GetQuoteCount]
(

)
RETURNS int
AS
BEGIN

	DECLARE @ItemCount int
	IF ([Identity].[IsUserInRole]('Quote Administrator') = 'true')
	BEGIN
		SET @ItemCount = (SELECT COUNT(QuoteId) FROM CoreModel.Quotes) 
	END
	ELSE
	BEGIN
		SET @ItemCount = (SELECT COUNT(QuoteId) FROM SecurityModel.PrincipalQuotes WHERE [PrincipalId] = DATABASE_PRINCIPAL_ID())
	END

	-- Return the result of the function
	RETURN @ItemCount;

END
GO
GRANT EXECUTE
    ON OBJECT::[CoreFacade].[GetQuoteCount] TO [AssureCustomer]
    AS [dbo];

