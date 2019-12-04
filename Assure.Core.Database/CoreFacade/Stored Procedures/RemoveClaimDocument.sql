
CREATE PROCEDURE [CoreFacade].[RemoveClaimDocument]
	@ClaimId int,
	@DocumentId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DELETE FROM CoreModel.ClaimDocuments
		WHERE ClaimId = @ClaimId
		  AND DocumentId = @DocumentId

END