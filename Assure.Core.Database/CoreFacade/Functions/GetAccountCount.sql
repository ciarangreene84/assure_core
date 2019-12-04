CREATE FUNCTION [CoreFacade].[GetAccountCount]
(

)
RETURNS int
AS
BEGIN

	DECLARE @ItemCount int
	IF ([Identity].[IsUserInRole]('Account Administrator') = 'true')
	BEGIN
		SET @ItemCount = (SELECT COUNT(AccountId) FROM CoreModel.Accounts) 
	END
	ELSE
	BEGIN
		SET @ItemCount = (SELECT COUNT(AccountId) FROM SecurityModel.PrincipalAccounts WHERE [PrincipalId] = DATABASE_PRINCIPAL_ID())
	END

	-- Return the result of the function
	RETURN @ItemCount;

END
GO
GRANT EXECUTE
    ON OBJECT::[CoreFacade].[GetAccountCount] TO [AssureCustomer]
    AS [dbo];

