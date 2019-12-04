CREATE FUNCTION [CoreFacade].[GetCustomerCount]
(

)
RETURNS int
AS
BEGIN

	DECLARE @ItemCount int
	IF ([Identity].[IsUserInRole]('Customer Administrator') = 'true')
	BEGIN
		SET @ItemCount = (SELECT COUNT(CustomerId) FROM CoreModel.Customers) 
	END
	ELSE
	BEGIN
		SET @ItemCount = (SELECT COUNT(CustomerId) FROM SecurityModel.PrincipalCustomers WHERE [PrincipalId] = DATABASE_PRINCIPAL_ID())
	END

	-- Return the result of the function
	RETURN @ItemCount;

END
GO
GRANT EXECUTE
    ON OBJECT::[CoreFacade].[GetCustomerCount] TO [AssureCustomer]
    AS [dbo];

