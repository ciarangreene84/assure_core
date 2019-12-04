
CREATE FUNCTION [CoreFacade].[GetLeadCount]
(

)
RETURNS int
AS
BEGIN

	DECLARE @ItemCount int
	IF ([Identity].[IsUserInRole]('Lead Administrator') = 'true')
	BEGIN
		SET @ItemCount = (SELECT COUNT(LeadId) FROM CoreModel.Leads) 
	END
	ELSE
	BEGIN
		SET @ItemCount = (SELECT COUNT(LeadId) FROM SecurityModel.PrincipalLeads WHERE [PrincipalId] = DATABASE_PRINCIPAL_ID())
	END

	-- Return the result of the function
	RETURN @ItemCount;

END
GO
GRANT EXECUTE
    ON OBJECT::[CoreFacade].[GetLeadCount] TO [AssureCustomer]
    AS [dbo];

