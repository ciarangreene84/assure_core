
CREATE PROCEDURE [CoreFacade].[RemovePolicyDocument]
	@PolicyId int,
	@DocumentId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DELETE FROM CoreModel.PolicyDocuments
		WHERE PolicyId = @PolicyId
		  AND DocumentId = @DocumentId

END