

CREATE FUNCTION [CoreFacade].[GetCustomerPolicies]
(	
	@CustomerId int
)
RETURNS TABLE 
AS
RETURN 
(
	SELECT Policies.*
		FROM CoreModel.CustomerPolicies
		INNER JOIN CoreFacade.Policies
				ON CustomerPolicies.PolicyId = Policies.PolicyId
		WHERE CustomerId = @CustomerId
)