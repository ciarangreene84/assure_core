

CREATE PROCEDURE [CoreFacade].[AddPolicyDocument]
	@PolicyId int,
	@DocumentId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO CoreModel.PolicyDocuments (PolicyId, DocumentId) VALUES (@PolicyId, @DocumentId)

END