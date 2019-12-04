-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE StaticFacade.RemoveProductBenefit
	@ProductId int,
	@BenefitId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DELETE FROM StaticModel.ProductBenefits 
		WHERE ProductId = @ProductId
		  AND BenefitId = @BenefitId

END