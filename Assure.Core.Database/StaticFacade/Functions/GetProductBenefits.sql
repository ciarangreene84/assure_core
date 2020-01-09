CREATE FUNCTION StaticFacade.GetProductBenefits
(	
	@ProductId int
)
RETURNS TABLE 
AS
RETURN 
(
	SELECT Benefits.*
		FROM  StaticModel.ProductBenefits 
		INNER JOIN StaticFacade.Benefits
				ON ProductBenefits.BenefitId = Benefits.BenefitId
		WHERE ProductId = @ProductId
)