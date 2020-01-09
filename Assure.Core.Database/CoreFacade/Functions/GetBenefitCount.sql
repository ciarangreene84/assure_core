
CREATE FUNCTION [CoreFacade].[GetBenefitCount]
(

)
RETURNS int
AS
BEGIN

	DECLARE @ItemCount int
	SET @ItemCount = (SELECT COUNT(BenefitId) FROM StaticModel.Benefits) 

	-- Return the result of the function
	RETURN @ItemCount;

END