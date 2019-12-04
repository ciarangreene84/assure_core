
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [CoreFacade].[RemoveCustomerPolicy]
	@CustomerId int,
	@PolicyId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DELETE FROM CoreModel.CustomerPolicies 
		WHERE CustomerId = @CustomerId
		  AND PolicyId = @PolicyId

END