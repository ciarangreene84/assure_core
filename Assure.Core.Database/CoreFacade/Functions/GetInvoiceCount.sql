CREATE FUNCTION [CoreFacade].[GetInvoiceCount]
(

)
RETURNS int
AS
BEGIN

	DECLARE @ItemCount int
	IF ([Identity].[IsUserInRole]('Invoice Administrator') = 'true')
	BEGIN
		SET @ItemCount = (SELECT COUNT(InvoiceId) FROM CoreModel.Invoices) 
	END
	ELSE
	BEGIN
		SET @ItemCount = (SELECT COUNT(InvoiceId) FROM SecurityModel.PrincipalInvoices WHERE [PrincipalId] = DATABASE_PRINCIPAL_ID())
	END

	-- Return the result of the function
	RETURN @ItemCount;

END
GO
GRANT EXECUTE
    ON OBJECT::[CoreFacade].[GetInvoiceCount] TO [AssureCustomer]
    AS [dbo];

