CREATE FUNCTION [CoreFacade].[GetAgentCount]
(

)
RETURNS int
AS
BEGIN

	DECLARE @ItemCount int
	IF ([Identity].[IsUserInRole]('Agent Administrator') = 'true')
	BEGIN
		SET @ItemCount = (SELECT COUNT(AgentId) FROM CoreModel.Agents) 
	END
	ELSE
	BEGIN
		SET @ItemCount = (SELECT COUNT(AgentId) FROM SecurityModel.PrincipalAgents WHERE [PrincipalId] = DATABASE_PRINCIPAL_ID())
	END

	-- Return the result of the function
	RETURN @ItemCount;

END
GO
GRANT EXECUTE
    ON OBJECT::[CoreFacade].[GetAgentCount] TO [AssureCustomer]
    AS [dbo];

