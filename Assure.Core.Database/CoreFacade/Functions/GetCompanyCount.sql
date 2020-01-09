
CREATE FUNCTION [CoreFacade].[GetCompanyCount]
(

)
RETURNS int
AS
BEGIN

	DECLARE @ItemCount int
	IF ([Identity].[IsUserInRole]('Company Administrator') = 'true')
	BEGIN
		SET @ItemCount = (SELECT COUNT(CompanyId) FROM CoreModel.Companies) 
	END
	ELSE
	BEGIN
		SET @ItemCount = (SELECT COUNT(CompanyId) FROM SecurityModel.PrincipalCompanies WHERE [PrincipalId] = DATABASE_PRINCIPAL_ID())
	END

	-- Return the result of the function
	RETURN @ItemCount;

END
GO
GRANT EXECUTE
    ON OBJECT::[CoreFacade].[GetCompanyCount] TO [AssureCustomer]
    AS [dbo];

