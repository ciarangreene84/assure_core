CREATE VIEW [StaticFacade].[Countries]
AS
SELECT [Alpha2],
       [Alpha3],
       [NumericCode],
       [Name]
FROM   [StaticModel].[Countries];