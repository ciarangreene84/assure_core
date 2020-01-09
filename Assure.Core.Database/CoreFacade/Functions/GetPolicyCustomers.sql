
CREATE FUNCTION [CoreFacade].[GetPolicyCustomers]
(	
	@PolicyId int
)
RETURNS TABLE 
AS
RETURN 
(
	SELECT Customers.*
		FROM CoreModel.CustomerPolicies
		INNER JOIN CoreFacade.Customers
				ON CustomerPolicies.CustomerId = Customers.CustomerId
		WHERE PolicyId = @PolicyId
)