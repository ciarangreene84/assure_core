CREATE PROCEDURE StaticFacade.AddProductBenefit
	@ProductId int,
	@BenefitId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO StaticModel.ProductBenefits (ProductId, BenefitId) VALUES (@ProductId, @BenefitId)

END