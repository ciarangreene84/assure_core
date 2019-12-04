
CREATE VIEW [StaticFacade].[Currencies]
WITH SCHEMABINDING
AS
	SELECT [Alpha3],
		   [Name]
		FROM [StaticModel].[Currencies];