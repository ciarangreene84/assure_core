
CREATE PROCEDURE [CoreFacade].[AddCustomerPolicy]
	@CustomerId int,
	@PolicyId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO CoreModel.CustomerPolicies (CustomerId, PolicyId) VALUES (@CustomerId, @PolicyId)

END