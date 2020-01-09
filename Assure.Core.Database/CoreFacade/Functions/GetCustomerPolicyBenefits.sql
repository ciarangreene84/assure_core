

CREATE FUNCTION [CoreFacade].[GetCustomerPolicyBenefits]
(	
	@CustomerId int,
	@PolicyId int
)
RETURNS TABLE 
AS
RETURN 
(
	SELECT Benefits.*
		FROM CoreModel.CustomerPolicyBenefits
		INNER JOIN [StaticFacade].Benefits
				ON CustomerPolicyBenefits.BenefitId = benefits.BenefitId
		WHERE CustomerId = @CustomerId
		  AND PolicyId = @PolicyId
)