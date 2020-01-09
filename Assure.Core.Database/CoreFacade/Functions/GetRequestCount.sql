
CREATE FUNCTION [CoreFacade].[GetRequestCount]
(

)
RETURNS int
AS
BEGIN

	DECLARE @ItemCount int
	IF ([Identity].[IsUserInRole]('Request Administrator') = 'true')
	BEGIN
		SET @ItemCount = (SELECT COUNT(RequestId) FROM CoreModel.Requests) 
	END
	ELSE
	BEGIN
		SET @ItemCount = (SELECT COUNT(RequestId) FROM SecurityModel.PrincipalRequests WHERE [PrincipalId] = DATABASE_PRINCIPAL_ID())
	END

	-- Return the result of the function
	RETURN @ItemCount;

END