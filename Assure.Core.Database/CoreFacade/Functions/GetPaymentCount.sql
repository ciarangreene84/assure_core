
CREATE FUNCTION [CoreFacade].[GetPaymentCount]
(

)
RETURNS int
AS
BEGIN

	DECLARE @ItemCount int
	IF ([Identity].[IsUserInRole]('Payment Administrator') = 'true')
	BEGIN
		SET @ItemCount = (SELECT COUNT(PaymentId) FROM CoreModel.Payments) 
	END
	ELSE
	BEGIN
		SET @ItemCount = (SELECT COUNT(PaymentId) FROM SecurityModel.PrincipalPayments WHERE [PrincipalId] = DATABASE_PRINCIPAL_ID())
	END

	-- Return the result of the function
	RETURN @ItemCount;

END
GO
GRANT EXECUTE
    ON OBJECT::[CoreFacade].[GetPaymentCount] TO [AssureCustomer]
    AS [dbo];

