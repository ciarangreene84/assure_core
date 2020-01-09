CREATE FUNCTION [CoreFacade].[GetCardCount]
(

)
RETURNS int
AS
BEGIN

	DECLARE @ItemCount int
	IF ([Identity].[IsUserInRole]('Card Administrator') = 'true')
	BEGIN
		SET @ItemCount = (SELECT COUNT(CardId) FROM CoreModel.Cards) 
	END
	ELSE
	BEGIN
		SET @ItemCount = (SELECT COUNT(CardId) FROM SecurityModel.PrincipalCards WHERE [PrincipalId] = DATABASE_PRINCIPAL_ID())
	END

	-- Return the result of the function
	RETURN @ItemCount;

END
GO
GRANT EXECUTE
    ON OBJECT::[CoreFacade].[GetCardCount] TO [AssureCustomer]
    AS [dbo];

