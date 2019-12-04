

CREATE PROCEDURE [CoreFacade].[AddCustomerPolicyBenefit]
	@CustomerId int,
	@PolicyId int,
	@BenefitId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO CoreModel.CustomerPolicyBenefits (CustomerId, PolicyId, BenefitId) VALUES (@CustomerId, @PolicyId, @BenefitId)

END