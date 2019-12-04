

CREATE FUNCTION [CoreFacade].[GetClaimCount]
(

)
RETURNS int
AS
BEGIN

	DECLARE @ItemCount int
	IF ([Identity].[IsUserInRole]('Claim Administrator') = 'true')
	BEGIN
		SET @ItemCount = (SELECT COUNT(ClaimId) FROM CoreModel.Claims) 
	END
	ELSE
	BEGIN
		SET @ItemCount = (SELECT COUNT(ClaimId) FROM SecurityModel.PrincipalClaims WHERE [PrincipalId] = DATABASE_PRINCIPAL_ID())
	END

	-- Return the result of the function
	RETURN @ItemCount;

END
GO
GRANT EXECUTE
    ON OBJECT::[CoreFacade].[GetClaimCount] TO [AssureCustomer]
    AS [dbo];

