

CREATE PROCEDURE [CoreFacade].[AddClaimDocument]
	@ClaimId int,
	@DocumentId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO CoreModel.ClaimDocuments (ClaimId, DocumentId) VALUES (@ClaimId, @DocumentId)

END