
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [Identity].[IsUserInRole]
(
	@Role nvarchar(256)
)
RETURNS bit
WITH SCHEMABINDING
AS
BEGIN

	DECLARE @IsUserInRole bit
	IF EXISTS (SELECT userRoles.RoleId
					FROM [Identity].UserRoles
					INNER JOIN [Identity].Roles
							ON UserRoles.RoleId = Roles.RoleId
					WHERE CONVERT(nvarchar(128), UserId) = USER_NAME()
					  AND roles.[Name] = @Role)
	BEGIN 
		SET @IsUserInRole  = CONVERT(bit, 'true')
	END
	ELSE 
	BEGIN 
		SET @IsUserInRole  = CONVERT(bit, 'false')
	END

	RETURN @IsUserInRole
END