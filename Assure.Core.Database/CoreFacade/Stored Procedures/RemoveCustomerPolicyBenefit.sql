

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [CoreFacade].[RemoveCustomerPolicyBenefit]
	@CustomerId int,
	@PolicyId int,
	@BenefitId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DELETE FROM CoreModel.CustomerPolicyBenefits 
		WHERE CustomerId = @CustomerId
		  AND PolicyId = @PolicyId
		  AND BenefitId = @BenefitId

END