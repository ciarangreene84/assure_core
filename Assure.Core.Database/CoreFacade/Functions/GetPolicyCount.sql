CREATE FUNCTION [CoreFacade].[GetPolicyCount]
(

)
RETURNS int
AS
BEGIN

	DECLARE @ItemCount int
	IF ([Identity].[IsUserInRole]('Policy Administrator') = 'true')
	BEGIN
		SET @ItemCount = (SELECT COUNT(PolicyId) FROM CoreModel.Policies) 
	END
	ELSE
	BEGIN
		SET @ItemCount = (SELECT COUNT(PolicyId) FROM SecurityModel.PrincipalPolicies WHERE [PrincipalId] = DATABASE_PRINCIPAL_ID())
	END

	-- Return the result of the function
	RETURN @ItemCount;

END
GO
GRANT EXECUTE
    ON OBJECT::[CoreFacade].[GetPolicyCount] TO [AssureCustomer]
    AS [dbo];

